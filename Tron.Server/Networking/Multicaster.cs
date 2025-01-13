using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Networking
{
    internal class Multicaster
    {
        public Socket Local { get; }

        private readonly EndPoint?[] _remotes;

        internal Multicaster(Socket local, EndPoint remote)
        {
            Local = local;
            
            _remotes = new EndPoint[2];
            _remotes[0] = remote;
        }

        public (Message?, EndPoint?) Receive()
        {
            EndPoint anyPointOrServer = new IPEndPoint(IPAddress.Any, 0);

            Message? message = Local.TryReceiveFrom(ref anyPointOrServer);

            if (_remotes.Contains(anyPointOrServer) || (anyPointOrServer as IPEndPoint)!.Address.Equals((Local.LocalEndPoint as IPEndPoint)!.Address))
            {
                return (message, anyPointOrServer);
            }

            return (null, null);
        }

        public bool SendTo(Message message, EndPoint remote)
        {
            return Local.TrySendTo(message, remote);
        }

        public bool AddRemote(EndPoint remote)
        {
            if (_remotes[1] != null) return false;

            _remotes[1] = remote;
            
            return true;
        }

        public void RemoveRemote()
        {
            _remotes[1] = null;
        }
    }
}
