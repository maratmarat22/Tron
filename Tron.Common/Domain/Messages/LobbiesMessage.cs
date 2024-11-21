namespace Tron.Common.Domain.Messages
{
    internal class LobbiesMessage : Message
    {
        public List<string> Lobbies { get; private set; }

        internal LobbiesMessage(Headers header, List<string> segments) : base(header, segments)
        {
            Lobbies = segments;
        }
    }
}
