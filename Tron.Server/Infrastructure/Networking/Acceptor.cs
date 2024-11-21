using System.Net;
using System.Net.Sockets;
using Tron.Common.Infrastructure.Networking;

namespace Tron.Server.Infrastructure.Networking
{
    internal class Acceptor
    {
        private Socket _listener;
        private IPAddress _serverIp;
        private int _freePort;

        internal Acceptor((string ip, int port) serverSocket)
        {
            _serverIp = IPAddress.Parse(serverSocket.ip);
            _freePort = serverSocket.port;

            EndPoint listenerPoint = new IPEndPoint(_serverIp, _freePort);

            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _listener.Bind(listenerPoint);
        }

        //internal void BroadcastNewSocket((string, int) socket)
        //{
        //    EndPoint everyone = new IPEndPoint(IPAddress.Broadcast, 0);
        //    _listener.SendTo(everyone, $"{Protocol.Reset}/{socket}");
        //}

        internal Connection Accept()
        {
            EndPoint anyPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = new byte[1024];
            _ = _listener.ReceiveFrom(data, ref anyPoint);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ++_freePort;
            server.Bind(new IPEndPoint(_serverIp, _freePort));
            return new Connection(server, anyPoint);
        }
    }
}
