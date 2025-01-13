using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Networking
{
    internal class Multicaster
    {
        public Socket Local { get; }

        internal EndPoint?[] Remotes { get; }

        internal Multicaster(Socket local, EndPoint remote)
        {
            Local = local;
            
            Remotes = new EndPoint[2];
            Remotes[0] = remote;
        }

        public void SendAll(Message message)
        {
            foreach (var remote in Remotes)
            {
                Local.TrySendTo(message, remote!);
            }
        }

        public bool SendTo(Message message, EndPoint remote) => Local.TrySendTo(message, remote);

        public (Message?, EndPoint?) Receive()
        {
            EndPoint anyPointOrServer = new IPEndPoint(IPAddress.Any, 0);

            Message? request = Local.TryReceiveFrom(ref anyPointOrServer);

            if (Remotes.Contains(anyPointOrServer) || (anyPointOrServer as IPEndPoint)!.Address.Equals((Local.LocalEndPoint as IPEndPoint)!.Address))
            {
                return (request, anyPointOrServer);
            }

            return (null, null);
        }

        public bool AddRemote(EndPoint remote)
        {
            if (Remotes[1] != null) return false;

            Remotes[1] = remote;
            
            return true;
        }

        public void RemoveRemote() => Remotes[1] = null;
    }
}
