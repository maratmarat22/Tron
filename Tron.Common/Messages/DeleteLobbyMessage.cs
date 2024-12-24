namespace Tron.Common.Messages
{
    public class DeleteLobbyMessage : Message
    {
        public string LobbyHostname { get; }

        public DeleteLobbyMessage(Header header, List<string> segments) : base(header, segments)
        {
            LobbyHostname = segments[0];
        }

        public DeleteLobbyMessage(string lobbyHost)
        {
            Header = Header.DELETE_LOBBY;
            LobbyHostname = lobbyHost;
        }

        public override string ToString() => $"{(int)Header}/{LobbyHostname}";
    }
}
