using System.Net;
using System.Net.Sockets;
using Tron.Common.Extensions;

namespace Tron.Server.Infrastructure.Network
{
    internal class UdpServer
    {
        private Socket _listener;
        private IPAddress _serverIp;
        private int _freePort;

        internal UdpServer((string ip, int port) serverSocket)
        {
            _serverIp = IPAddress.Parse(serverSocket.ip);
            _freePort = serverSocket.port;
        }

        internal void Launch()
        {
            EndPoint listenerPoint = new IPEndPoint(_serverIp, _freePort);
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _listener.Bind(listenerPoint);
        }

        internal ClientHandler Listen()
        {
            if (_listener == null) throw new InvalidOperationException("Listener was not initialized");

            EndPoint anyPoint = new IPEndPoint(IPAddress.Any, 0);
            
            string[] clientInfo = _listener!.ReceiveFrom(ref anyPoint).Split('/');

            EndPoint clientPoint = new IPEndPoint(IPAddress.Parse(clientInfo[0]), Convert.ToInt32(clientInfo[1]));
            Socket serverOperator = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ++_freePort;
            serverOperator.Bind(new IPEndPoint(_serverIp, _freePort));

            return new ClientHandler(serverOperator, clientPoint);
        }
    }
}
