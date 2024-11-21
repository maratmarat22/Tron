using System.Net;
using System.Net.Sockets;

namespace Tron.Common.Infrastructure.Networking
{
    public struct Connection
    {
        public Socket Local { get; private set; }

        public EndPoint Remote { get; private set; }

        public Connection(Socket local, EndPoint remote)
        {
            Local = local;
            Remote = remote;
        }
    }
}
