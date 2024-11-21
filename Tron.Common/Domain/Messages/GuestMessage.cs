namespace Tron.Common.Domain.Messages
{
    internal class GuestMessage : Message
    {
        public int LobbyId { get; private set; }

        internal GuestMessage(Headers header, List<string> segments) : base(header, segments)
        {
            LobbyId = int.Parse(segments[0]);
        }
    }
}
