using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Networking
{
    internal class UdpAcceptor
    {
        private readonly Socket _acceptor;
        private readonly IPAddress _address;
        private int _availablePort;

        private IDbQueryProcessor _queryProcessor;

        internal UdpAcceptor(IPEndPoint point, IDbQueryProcessor queryProcessor)
        {
            _acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _acceptor.Bind(point);

            //
            Console.WriteLine($"Acceptor: listening on port {point.Port}");
            //

            _address = point.Address;
            _availablePort = point.Port + 1;

            _queryProcessor = queryProcessor;
        }

        internal UdpUnicaster Accept()
        {
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            AuthentificationMessage auth = (AuthentificationMessage)_acceptor.SafeReceiveFrom(ref remote, Header.REGISTER, Header.LOGIN);

            bool succes = auth.Header switch
            {
                Header.REGISTER => _queryProcessor.TryRegister(auth.Username),
                Header.LOGIN => _queryProcessor.TryLogIn(auth.Username),
                _ => false
            };

            if (succes)
            {
                UdpUnicaster? unicaster = Redirect(remote);

                if (unicaster != null)
                {
                    return unicaster;
                }
                else
                {
                    _acceptor.SafeSendTo(new Message(Header.INTERNAL_SERVER_ERROR), remote);
                    throw new Exception();
                }
            }
            else
            {
                _acceptor.SafeSendTo(new Message(Header.BAD_REQUEST), remote);
                throw new Exception();
            }
        }

        private UdpUnicaster? Redirect(EndPoint remote)
        {
            Socket local = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            if (local.TryBind(_address, _availablePort))
            {

                //
                Console.WriteLine($"Server: listening on port {_availablePort}");
                //

                IPEndPoint localPoint = ((IPEndPoint)local.LocalEndPoint!);

                _availablePort = localPoint.Port + 1;

                IPEndPointMessage redirect = new(Header.REDIRECT, localPoint.Address, localPoint.Port);

                _acceptor.SafeSendTo(redirect, remote);
                local.SafeReceiveFrom(ref remote, Header.CONNECT);

                //
                Console.WriteLine($"{remote}: connected to {localPoint.Port}");
                //

                return new UdpUnicaster(local, (IPEndPoint)remote);
            }
            else
            {
                return null;
            }
        }
    }
}
