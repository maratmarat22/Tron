namespace Tron.Common.Messages
{
    public class AcknowledgeMessage : Message
    {
        public Header AcknowledgedHeader{ get; }

        public AcknowledgeMessage(Header header, List<string> segments) : base(header, segments)
        {
            AcknowledgedHeader = (Header)int.Parse(segments[0]);
        }

        public AcknowledgeMessage(Header ackedHeader)
        {
            Header = Header.ACKNOWLEDGE;
            AcknowledgedHeader = ackedHeader;
        }

        public override string ToString() => $"{(int)Header}/{(int)AcknowledgedHeader}";
    }
}
