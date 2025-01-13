using System.Text;

namespace Tron.Common.Messages
{
    public class Message(Header header, string[] payload)
    {
        public Header Header { get; set; } = header;

        public string[] Payload { get; set; } = payload;

        public override string ToString()
        {
            StringBuilder message = new();

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
