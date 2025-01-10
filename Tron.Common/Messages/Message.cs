using System.Text;

namespace Tron.Common.Messages
{
    public class Message
    {
        public Header Header { get; set; }

        public string[] Payload { get; set; }

        public Message(Header header, string[] payload)
        {
            Header = header;
            Payload = payload;
        }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();

            message.Append((int)Header);

            if (Payload != null)
            {
                foreach (string segment in Payload)
                {
                    message.Append($"/{segment}");
                }
            }

            return message.ToString();
        }
    }
}
