namespace Tron.Common.Messages
{
    public class CreateLobbyMessage : Message
    {
        public string Hostname { get; }

        public bool IsPrivate { get; }

        public string Password { get; }

        public CreateLobbyMessage(Header header, List<string> segments) : base(header, segments)
        {
            Hostname = segments[0];
            IsPrivate = bool.Parse(segments[1]);
            Password = segments[2];
        }

        public CreateLobbyMessage(string host, bool isPrivate, string password)
        {
            Header = Header.CREATE_LOBBY;

            Hostname = host;
            IsPrivate = isPrivate;
            Password = password;
        }

        public override string ToString() => $"{(int)Header}/{Hostname}/{IsPrivate}/{Password}";
    }
}
