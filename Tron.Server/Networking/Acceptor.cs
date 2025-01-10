using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Networking
{
    internal class Acceptor
    {
        private readonly Socket _acceptor;
        private readonly IPAddress _address;
        private int _availablePort;

        private IDbQueryProcessor _queryProcessor;

        internal Acceptor(IPEndPoint point, IDbQueryProcessor queryProcessor)
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

        internal Unicaster? Accept()
        {
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            Message? auth = _acceptor.TryReceiveFrom(ref remote);

            bool authorized = false;

            if (auth != null)
            {
                authorized = auth.Header switch
                {
                    Header.Register => _queryProcessor.TryRegister(auth.Payload[0]),
                    Header.LogIn => _queryProcessor.TryLogIn(auth.Payload[0]),
                    _ => false
                };
            }

            if (authorized)
            {
                Unicaster? unicaster = TryRedirect(remote, auth!.Header);

                if (unicaster == null)
                {
                    _acceptor.TrySendTo(new Message(Header.INTERNAL_SERVER_ERROR, []), remote);
                }

                return unicaster;
            }
            
            _acceptor.TrySendTo(new Message(Header.BAD_REQUEST, []), remote);
            
            return null;
        }

        private Unicaster? TryRedirect(EndPoint remote, Header prevHeader)
        {
            Socket local = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            if (local.TryBind(_address, _availablePort))
            {

                //
                Console.WriteLine($"Server socket: listening on port {_availablePort}");
                //

                IPEndPoint localPoint = ((IPEndPoint)local.LocalEndPoint!);

                _availablePort = localPoint.Port + 1;

                if (_acceptor.TrySendTo(new Message(Header.Acknowledge, [prevHeader.ToString(), localPoint.ToString()]), remote))
                {
                    Message? connect = local.TryReceiveFrom(ref remote);

                    if (connect != null)
                    {
                        //
                        Console.WriteLine($"{remote}: connected to {localPoint.Port}");
                        //

                        return new Unicaster(local, (IPEndPoint)remote);
                    }
                }
            }

            return null;
        }
    }
}
