using System.Net;
using System.Net.Sockets;
using System.Text;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public static class MessageSocket
    {
        public static void SendTo(this Socket local, EndPoint remote, Message message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message.ToString());
            local.SendTo(bytes, remote);

            Message status = local.ReceiveFrom(ref remote);

            while (status.Header != Header.OK)
            {
                local.SendTo(bytes, remote);
            }
        }

        public static Message ReceiveFrom(this Socket local, ref EndPoint remote)
        {
            byte[] bytes = new byte[1024];
            local.ReceiveFrom(bytes, ref remote);
            string message = Encoding.UTF8.GetString(bytes);

            return new Message(message);
        }

        public static Message ReceiveFrom(this Socket local, EndPoint remote)
        {
            byte[] bytes = new byte[1024];
            local.ReceiveFrom(bytes, ref remote);
            string message = Encoding.UTF8.GetString(bytes);

            return new Message(message);
        }
    }
}
