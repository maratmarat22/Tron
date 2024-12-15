namespace Tron.Common.Messages
{
    public class JoinLobbyMessage : Message
    {
        public int LobbyId { get; }

        public JoinLobbyMessage(Header header, List<string> segments) : base(header, segments)
        {
            LobbyId = int.Parse(segments[0]);
        }

        public JoinLobbyMessage(int lobbyId)
        {
            Header = Header.JoinLobby;
            LobbyId = lobbyId;
        }

        public override string ToString() => Header.ToString() + '/' + LobbyId.ToString();
    }
}
