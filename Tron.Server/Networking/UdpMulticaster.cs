using System.Net;
using System.Net.Sockets;
using Tron.Common.Networking;

namespace Tron.Server.Networking
{
    internal class UdpMulticaster
    {
        public UdpClient Local { get; private set; }

        public List<IPEndPoint> Remotes { get; private set; }

        internal UdpMulticaster(UdpClient local, params IPEndPoint[] remotes)
        {
            Local = local;
            Remotes = [.. remotes];
        }

        internal UdpMulticaster(UdpUnicaster unicaster)
        {
            Local = unicaster.Local;
            Remotes = [unicaster.Remote];
        }
    }
}
