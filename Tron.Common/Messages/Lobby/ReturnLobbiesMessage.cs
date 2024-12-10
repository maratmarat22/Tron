using Tron.Common.Messages.General;

namespace Tron.Common.Messages.Lobby
{
    public class ReturnLobbiesMessage : Message
    {
        public List<string> Lobbies { get; private set; }

        public ReturnLobbiesMessage(Header header, List<string> segments) : base(header, segments)
        {
            Lobbies = segments;
        }
    }
}
