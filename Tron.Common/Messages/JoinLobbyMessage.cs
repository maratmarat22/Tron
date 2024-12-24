namespace Tron.Common.Messages
{
    public class JoinLobbyMessage : Message
    {
        public string LobbyHostname { get; }

        public JoinLobbyMessage(Header header, List<string> segments) : base(header, segments)
        {
            LobbyHostname = segments[0];
        }

        public JoinLobbyMessage(string lobbyHost)
        {
            Header = Header.JOIN_LOBBY;
            LobbyHostname = lobbyHost;
        }

        public override string ToString() => $"{(int)Header}/{LobbyHostname}";
    }
}
