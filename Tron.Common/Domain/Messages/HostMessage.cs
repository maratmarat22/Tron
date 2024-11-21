namespace Tron.Common.Domain.Messages
{
    public class HostMessage : Message
    {
        public int MaxPlayers { get; private set; }

        public bool Private { get; private set; }

        public int Password { get; private set; }

        internal HostMessage(Headers header, List<string> segments) : base(header, segments)
        {
            MaxPlayers = int.Parse(segments[0]);
            Private = bool.Parse(segments[1]);
            Password = int.Parse(segments[2]);
        }
    }
}
