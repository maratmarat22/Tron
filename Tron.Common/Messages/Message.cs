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

            message.Append((int)Header);

            if (_segments != null)
            {
                foreach (string segment in _segments)
                {
                    message.Append($"/{segment}");
                }
            }

            return message.ToString();
        }

        public static bool TryDefine(Message message, out Message? result)
        {
            bool casted = true;
            try
            {
                result = message.Header switch
                {
                    Header.REGISTER or Header.LOGIN => new AuthentificationMessage(message.Header, message._segments!),
                    Header.REDIRECT => new IPEndPointMessage(message.Header, message._segments!),
                    Header.CONNECT => message,
                    Header.CREATE_LOBBY => new CreateLobbyMessage(message.Header, message._segments!),
                    Header.READ_LOBBIES => new ReadLobbiesMessage(message.Header, message._segments!),
                    Header.JOIN_LOBBY => new JoinLobbyMessage(message.Header, message._segments!),
                    Header.ACKNOWLEDGE => new AcknowledgeMessage(message.Header, message._segments!),
                    Header.RESEND => message,
                    _ => throw new InvalidCastException()
                };
            }
            catch (Exception)
            {
                casted = false;
                result = null;
            }

            return casted;
        }
    }
}
