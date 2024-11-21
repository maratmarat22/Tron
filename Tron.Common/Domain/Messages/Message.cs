namespace Tron.Common.Domain.Messages
{
    public abstract class Message
    {
        public Headers Header { get; private set; }

        protected List<string> _segments;

        public Message(Headers header, List<string> segments)
        {
            Header = header;
            _segments = segments;
        }

        public Message(Headers header)
        {
            Header = header;
            _segments = [];
        }
    }
}
