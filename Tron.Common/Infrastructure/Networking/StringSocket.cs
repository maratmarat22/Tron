using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tron.Common.Infrastructure.Networking
{
    public static class StringSocket
    {
        public static void SendTo(this Socket addresser, EndPoint addressee, string message)
        {
            byte[] buffer = new byte[1024];
            buffer = Encoding.ASCII.GetBytes(message);

            addresser.SendTo(buffer, addressee);
        }

        public static string ReceiveFrom(this Socket addressee, EndPoint addresser)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = addressee.ReceiveFrom(buffer, ref addresser);

            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }
    }
}
