using System.Net;
using System.Net.Sockets;

namespace Tron.Server.Domain
{
    internal class Connection
    {
        public Socket Server { get; private set; }

        public EndPoint Client { get; private set; }

        internal Connection(Socket server, EndPoint client)
        {
            Server = server;
            Client = client;
        }
    }
}
