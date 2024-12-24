using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public class UdpUnicaster
    {
        public Socket Local { get; private set; }

        private EndPoint _remote;
        public ref EndPoint Remote
        {
            get => ref _remote;
        }

        public UdpUnicaster(Socket local, EndPoint remote)
        {
            Local = local;
            //Local.ReceiveTimeout = 3000;
            _remote = remote;
            Remote = _remote;
        }

        public void Send(Message message)
        {
            Local.SafeSendTo(message, Remote);
        }

        public Message Receive(params Header[] expected)
        {
            return Local.SafeReceiveFrom(ref Remote, expected);
        }
    }
}
