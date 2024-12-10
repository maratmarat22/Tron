using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tron.Common.Networking
{
    public static class StringSocket
    {
        public static void SendString(this UdpClient local, IPEndPoint remote, string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            local.Send(bytes, remote);
        }

        public static string ReceiveString(this UdpClient local, IPEndPoint remote)
        {
            byte[] bytes = local.Receive(ref remote);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
