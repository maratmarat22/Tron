using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Client.Networking
{
    public class UdpConnector
    {
        private EndPoint _acceptor;

        public UdpConnector(IPAddress address, int port)
        {
            _acceptor = new IPEndPoint(address, port);
        }

        public UdpUnicaster Connect()
        {
            Socket local = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            local.Bind(new IPEndPoint(IPAddress.Loopback, 49152));

            local.SendTo(_acceptor, new Message(Header.Connect));

            EndPointMessage redirect = (EndPointMessage)local.ReceiveFrom(_acceptor);
            IPEndPoint remote = new IPEndPoint(redirect.Address, redirect.Port);
            
            local.SendTo(remote, new Message(Header.Connect));

            return new UdpUnicaster(local, remote);
        }
    }
}
