using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public class UdpUnicaster
    {
        public Socket Local { get; private set; }

        public IPEndPoint Remote { get; private set; }

        public UdpUnicaster(Socket local, IPEndPoint remote)
        {
            Local = local;
            Remote = remote;
        }

        public void Send(Message message)
        {
            Local.SendTo(Remote, message);
        }

        public Message Receive()
        {
            return Local.ReceiveFrom(Remote);
        }
    }
}
