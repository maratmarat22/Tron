using Tron.Common.Messages.General;

namespace Tron.Common.Messages.Game
{
    public class DirectionMessage : Message
    {
        public Direction Direction { get; private set; }

        public DirectionMessage(Header header, List<string> segments) : base(header, segments)
        {
            Direction = (Direction)int.Parse(segments[0]);
        }
    }
}
