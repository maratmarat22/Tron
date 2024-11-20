using System.Net;
using System.Net.Sockets;
using Tron.Common.Extensions;
using Tron.Common.Resources;
using Tron.Server.Domain;

namespace Tron.Server.Infrastructure.Network
{
    internal class UdpListener
    {
        private Socket _listener;
        private IPAddress _serverIp;
        private int _freePort;

        internal UdpListener((string ip, int port) serverSocket)
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

        internal Connection Listen(IDbManager dbManager)
        {
            EndPoint anyPoint = new IPEndPoint(IPAddress.Any, 0);
            
            string[] clientInfo = _listener.ReceiveFrom(ref anyPoint).Split('/');

            if (int.Parse(clientInfo[0]) == (int)Protocol.Connect)
            {
                EndPoint client = new IPEndPoint(IPAddress.Parse(clientInfo[0]), Convert.ToInt32(clientInfo[1]));
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                ++_freePort;
                server.Bind(new IPEndPoint(_serverIp, _freePort));
                return new Connection(server, client);
            }
            else throw new Exception();
        }
    }
}
