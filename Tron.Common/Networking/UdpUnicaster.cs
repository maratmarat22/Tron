using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public class UdpUnicaster
    {
        public System.Net.Sockets.Socket Local { get; private set; }

        private EndPoint _remote;
        public ref EndPoint Remote
        {
            get => ref _remote;
        }

        public UdpUnicaster(System.Net.Sockets.Socket local, EndPoint remote)
        {
            Local = local;
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
