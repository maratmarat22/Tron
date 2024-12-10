using Tron.Common.Messages.General;

namespace Tron.Common.Messages.Lobby
{
    public class CreateLobbyMessage : Message
    {
        public int MaxPlayers { get; private set; }

        public bool Private { get; private set; }

        public string Password { get; private set; }

        public CreateLobbyMessage(Header header, List<string> segments) : base(header, segments)
        {
            MaxPlayers = int.Parse(segments[0]);
            Private = bool.Parse(segments[1]);
            Password = segments[2];
        }
    }
}
