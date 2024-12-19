using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Networking
{
    internal class UdpAcceptor
    {
        private readonly Socket _acceptor;
        private readonly IPAddress _address;
        private int _availablePort;
        private int _connectionId;

        internal UdpAcceptor(IPEndPoint point)
        {
            _acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _acceptor.Bind(point);

            //
            Console.WriteLine($"Acceptor: listening on port {point.Port}");
            //

            _address = point.Address;
            _availablePort = point.Port + 1;
            _connectionId = 0;
        }

        internal UdpUnicaster Accept()
        {
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            _acceptor.SafeReceiveFrom(ref remote, Header.CONNECT);

            Socket local = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            if (local.TryBind(_address, _availablePort))
            {

                //
                Console.WriteLine($"Server: listening on port {_availablePort}");
                //
                
                IPEndPoint localPoint = ((IPEndPoint)local.LocalEndPoint!);

                _availablePort = localPoint.Port + 1;
                
                EndPointMessage redirect = new(Header.REDIRECT, localPoint.Address, localPoint.Port);
                
                _acceptor.SafeSendTo(redirect, remote);
                local.SafeReceiveFrom(ref remote, Header.CONNECT);

                //
                Console.WriteLine($"{remote}: connected to {localPoint.Port}");
                //

                return new UdpUnicaster(local, (IPEndPoint)remote);
            }

            _acceptor.SafeSendTo(new Message(Header.INTERNAL_SERVER_ERROR), remote);
            
            throw new Exception();
        }
    }
}
