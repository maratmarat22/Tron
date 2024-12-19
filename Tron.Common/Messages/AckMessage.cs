namespace Tron.Common.Messages
{
    public class AckMessage : Message
    {
        public Header AckedHeader{ get; }

        public AckMessage(Header header, List<string> segments) : base(header, segments)
        {
            AckedHeader = (Header)int.Parse(segments[0]);
        }

        public AckMessage(Header ackedHeader)
        {
            Header = Header.ACK;
            AckedHeader = ackedHeader;
        }

        public override string ToString() => ((int)Header).ToString() + '/' + ((int)AckedHeader).ToString();
    }
}
