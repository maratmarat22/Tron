using System.Net;
using System.Net.Sockets;
using Tron.Common.Networking.P2P;
using Tron.Common.Networking.Extensions;

namespace Tron.Client.Networking
{
    public class UdpConnector
    {
        private IPEndPoint _serverPoint;

        public UdpConnector((string address, int port) socket)
        {
            _serverPoint = new IPEndPoint(IPAddress.Parse(socket.address), socket.port);
        }

        public UdpUnicaster Connect()
        {
            UdpClient local = new UdpClient();

            local.SendString(_serverPoint, "HELLO");
            
            return new UdpUnicaster(local, _serverPoint);
        }
    }
}
