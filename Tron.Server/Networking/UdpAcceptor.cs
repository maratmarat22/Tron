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

        internal UdpAcceptor(IPEndPoint point)
        {
            _acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _acceptor.Bind(point);
            _address = point.Address;
            _availablePort = point.Port + 1;
        }

        internal UdpUnicaster Accept()
        {
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            _ = _acceptor.ReceiveFrom(ref remote);

            Socket local = ProvideSocket();
            IPEndPoint localPoint = (IPEndPoint)local.LocalEndPoint!;
            EndPointMessage redirect = new(Header.Redirect, localPoint.Address, localPoint.Port);

            _acceptor.SendTo(remote, redirect);

            _ = local.ReceiveFrom(remote);

            return new UdpUnicaster(local, (IPEndPoint)remote);
        }

        private Socket ProvideSocket()
        {
            Socket local = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            bool portAvailable = false;

            while (!portAvailable)
            {
                try
                {
                    IPEndPoint point = new(_address, _availablePort);
                    local.Bind(point);
                    portAvailable = true;
                }
                catch (SocketException)
                {
                    ++_availablePort;
                }

                ++_availablePort;
            }

            return local!;
        }
    }
}
