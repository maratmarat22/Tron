using Tron.Common.Messages.General;

namespace Tron.Common.Messages.Lobby
{
    public class JoinLobbyMessage : Message
    {
        public int LobbyId { get; private set; }

        public JoinLobbyMessage(Header header, List<string> segments) : base(header, segments)
        {
            LobbyId = int.Parse(segments[0]);
        }
    }
}
