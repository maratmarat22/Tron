using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Networking
{
    internal class UdpMulticaster
    {
        public System.Net.Sockets.Socket Local { get; private set; }

        public List<EndPoint> Remotes { get; private set; }

        internal UdpMulticaster(System.Net.Sockets.Socket local, params IPEndPoint[] remotes)
        {
            Local = local;
            Remotes = [.. remotes];
        }

        internal UdpMulticaster(UdpUnicaster unicaster)
        {
            Local = unicaster.Local;
            Remotes = [unicaster.Remote];
        }

        internal void SendAll(Message message)
        {
            foreach (IPEndPoint remote in Remotes)
            {
                Local.SafeSendTo(message, remote);
            }
        }

        internal Message ReceiveAll()
        {
            throw new NotImplementedException();
        }
    }
}
