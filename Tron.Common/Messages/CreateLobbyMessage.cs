namespace Tron.Common.Messages
{
    public class CreateLobbyMessage : Message
    {
        public int MaxPlayers { get; }

        public bool IsPrivate { get; }

        public string Password { get; }

        public CreateLobbyMessage(Header header, List<string> segments) : base(header, segments)
        {
            MaxPlayers = int.Parse(segments[0]);
            IsPrivate = bool.Parse(segments[1]);
            Password = segments[2];
        }

        public CreateLobbyMessage(int maxPlayers, bool isPrivate, string password)
        {
            Header = Header.CREATE_LOBBY;
            MaxPlayers = maxPlayers;
            IsPrivate = isPrivate;
            Password = password;
        }

        public override string ToString() => Header.ToString() + '/' + MaxPlayers.ToString() + '/' + IsPrivate.ToString() + '/' + Password.ToString();
    }
}
