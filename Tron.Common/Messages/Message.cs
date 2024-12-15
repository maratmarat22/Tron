using System.Text;

namespace Tron.Common.Messages
{
    public class Message
    {
        public Header Header { get; protected set; }

        private readonly List<string>? _segments;

        internal Message(Header header, List<string> segments)
        {
            Header = header;
            _segments = segments;
        }

        public Message(Header header)
        {
            Header = header;
        }

        public Message(string message)
        {
            string[] strings = message.Split('/');
            Header = (Header)int.Parse(strings[0]);
            _segments = [.. strings[1..]];
        }

        internal Message() { }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();

            message.Append(Header.ToString());

            if (_segments != null)
            {
                foreach (string segment in _segments)
                {
                    message.Append($"/{segment}");
                }
            }

            return message.ToString();
        }
    }
}
