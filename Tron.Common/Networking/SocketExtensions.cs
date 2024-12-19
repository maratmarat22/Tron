using System.Net;
using System.Net.Sockets;
using System.Text;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public static class SocketExtensions
    {
        private static void SendTo(this Socket local, Message message, EndPoint remote)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message.ToString());
            local.SendTo(bytes, remote);
        }

        private static Message ReceiveFrom(this Socket local, ref EndPoint remote)
        {
            byte[] bytes = new byte[1024];
            int length = local.ReceiveFrom(bytes, ref remote);

            return new Message(Encoding.UTF8.GetString(bytes, 0, length));
        }

        public static void SafeSendTo(this Socket local, Message message, EndPoint remote)
        {
            local.SendTo(message, remote);

            Message response = local.SafeReceiveFrom(ref remote, message.Header);

            if (response.Header == Header.BAD_REQUEST)
            {
                
                //
                throw new Exception();
                //
            
            }

            while (response.Header != Header.ACK && (response as AckMessage)!.AckedHeader != message.Header)
            {
                local.SendTo(message, remote);
                response = local.SafeReceiveFrom(ref remote, message.Header);
            }
        }

        public static Message SafeReceiveFrom(this Socket local, ref EndPoint remote, params Header[] expected)
        {
            Message concrete;

            while (true)
            {
                Message raw = local.ReceiveFrom(ref remote);

                if (Message.TryDefine(raw, out concrete!))
                {
                    if (concrete.Header == Header.ACK && expected.Contains((concrete as AckMessage)!.AckedHeader))
                    {
                        break;
                    }
                    else if (!expected.Contains(concrete.Header))
                    {
                        local.SendTo(new Message(Header.BAD_REQUEST), remote);
                    }

                    local.SendTo(new AckMessage(concrete.Header), remote);
                    break;
                }

                local.SendTo(new Message(Header.RESEND), remote);
            }

            return concrete;
        }

        public static bool TryBind(this Socket socket, IPAddress address, int port)
        {
            bool portAvailable = false;

            while (!portAvailable)
            {
                portAvailable = true;

                try
                {
                    IPEndPoint point = new(address, port);
                    socket.Bind(point);
                }
                catch (SocketException)
                {
                    portAvailable = false;
                    ++port;
                }
            }

            ++port;

            return portAvailable;
        }
    }
}
