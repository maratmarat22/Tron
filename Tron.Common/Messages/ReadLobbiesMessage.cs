namespace Tron.Common.Messages
{
    public class ReadLobbiesMessage : Message
    {
        public bool IsPrivate { get; }

        public ReadLobbiesMessage(Header header, List<string> segments) : base(header, segments)
        {
            IsPrivate = bool.Parse(segments[0]);
        }

        public ReadLobbiesMessage(bool isPrivate)
        {
            Header = Header.READ_LOBBIES;
            IsPrivate = isPrivate;
        }

        public override string ToString() => $"{(int)Header}/{IsPrivate}";
    }
}
