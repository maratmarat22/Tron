using System.Net;
using System.Net.Sockets;
using Tron.Common.Networking;

namespace Tron.Client.Networking
{
    public class UdpConnector
    {
        private IPEndPoint _acceptor;

        public UdpConnector((string address, int port) acceptor)
        {
            _acceptor = new IPEndPoint(IPAddress.Parse(acceptor.address), acceptor.port);
        }

        public UdpUnicaster Connect()
        {
            UdpClient local = new UdpClient();

            local.SendString(_acceptor, "hi");
            string data = local.ReceiveString(_acceptor);
            string[] segments = data.Split('/');
            IPEndPoint remote = new IPEndPoint(IPAddress.Parse(segments[0]), int.Parse(segments[1]));

            return new UdpUnicaster(local, remote);
        }
    }
}
