namespace Tron.Common.Messages
{
    public class ReadLobbiesMessage : Message
    {
        public int MaxPlayers { get; }

        public bool IsPrivate { get; }

        public ReadLobbiesMessage(Header header, List<string> segments) : base(header, segments)
        {
            MaxPlayers = int.Parse(segments[0]);
            IsPrivate = bool.Parse(segments[1]);
        }

        public ReadLobbiesMessage(int maxPlayers, bool isPrivate)
        {
            Header = Header.ReadLobbies;
            MaxPlayers = maxPlayers;
            IsPrivate = isPrivate;
        }

        public override string ToString() => Header.ToString() + '/' + MaxPlayers.ToString() + '/' + IsPrivate.ToString();
    }
}
