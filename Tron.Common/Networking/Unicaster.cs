using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public class Unicaster(Socket local, EndPoint remote)
    {
        public Socket Local { get; } = local;

        public EndPoint Remote { get; } = remote;

        public bool Send(Message message)
        {
            return Local.TrySendTo(message, Remote);
        }

        public Message? Receive()
        {
            return Local.TryReceiveFrom(Remote);
        }
    }
}
