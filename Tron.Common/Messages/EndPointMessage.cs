using System.Net;

namespace Tron.Common.Messages
{
    public class EndPointMessage : Message
    {
        public IPAddress Address { get; }

        public int Port { get; }

        public EndPointMessage(Header header, List<string> segments) : base(header, segments)
        {
            Address = IPAddress.Parse(segments[0]);
            Port = int.Parse(segments[1]);
        }

        public EndPointMessage(Header header, IPAddress address, int port)
        {
            Header = header;
            Address = address;
            Port = port;
        }

        public override string ToString() => Header.ToString() + '/' + Address.ToString() + '/' + Port.ToString();
    }
}
