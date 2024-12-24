namespace Tron.Common.Messages
{
    public class AuthentificationMessage : Message
    {
        public string Username { get; }

        public AuthentificationMessage(Header header, List<string> segments) : base(header, segments)
        {
            Username = segments[0];
        }

        public AuthentificationMessage(Header header, string username)
        {
            Header = header;
            Username = username;
        }

        public override string ToString() => $"{(int)Header}/{Username}";
    }
}
