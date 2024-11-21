using System.Net;
using System.Net.Sockets;

namespace Tron.Server.Domain
{
    internal struct Multiconnection
    {
        public Socket Local { get; private set; }

        public List<Remote> Remotes { get; private set; }

        internal Multiconnection(Socket local, params Remote[] remotes)
        {
            Local = local;
            Remotes = remotes.ToList();
        }
    }
}
