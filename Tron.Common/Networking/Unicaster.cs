using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public class Unicaster : ICaster
    {
        public Socket Local { get; private set; }

        public EndPoint Remote { get; private set; }

        public Unicaster(Socket local, EndPoint remote)
        {
            Local = local;
            Remote = remote;
        }

        public bool Send(Message message)
        {
            return Local.TrySendTo(message, Remote);
        }

        public Message? Receive()
        {
            return Local.TryReceiveFrom(Remote);
        }

        public bool SendTo(Message message, EndPoint remote)
        {
            return Local.TrySendTo(message, remote);
        }
    }
}
