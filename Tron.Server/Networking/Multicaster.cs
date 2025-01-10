using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Networking
{
    internal class Multicaster
    {
        public Socket Local { get; }

        private List<EndPoint> _remotes;

        internal Multicaster(Socket local, EndPoint points)
        {
            Local = local;
            _remotes = [];
            _remotes.Add(points);
        }

        public bool Send(Message message)
        {
            List<bool> allSent = [];
            
            foreach (EndPoint point in _remotes)
            {
                if (Local.TrySendTo(message, point))
                {
                    return false;
                }
            }

            return true;
        }

        public (Message?, EndPoint?) Receive()
        {
            EndPoint AnyPointOrServer = new IPEndPoint(IPAddress.Any, 0);

            Message? message = Local.TryReceiveFrom(ref AnyPointOrServer);

            if (_remotes.Contains(AnyPointOrServer) || (AnyPointOrServer as IPEndPoint)!.Address.Equals((Local.LocalEndPoint as IPEndPoint)!.Address))
            {
                return (message, AnyPointOrServer);
            }

            return (null, null);
        }

        public bool SendTo(Message message, EndPoint remote)
        {
            return true;
        }

        public bool AddRemote(EndPoint remote)
        {
            if (_remotes.Count() < 2)
            {
                _remotes.Add(remote);
                return true;
            }

            return false;
        }
    }
}
