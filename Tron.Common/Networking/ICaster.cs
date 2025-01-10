using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public interface ICaster
    {
        public Socket Local { get; }
        
        public bool Send(Message message);

        public Message? Receive();

        public bool SendTo(Message message, EndPoint remote);
    }
}
