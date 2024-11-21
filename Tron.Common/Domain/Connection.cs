using System.Net;
using System.Net.Sockets;

namespace Tron.Common.Domain
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
