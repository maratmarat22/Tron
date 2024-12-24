using Tron.Common.Entities;

namespace Tron.Common.Messages
{
    public class DirectionMessage : Message
    {
        public Direction Direction { get; }

        public DirectionMessage(Header header, List<string> segments) : base(header, segments)
        {
            Direction = (Direction)int.Parse(segments[0]);
        }

        public DirectionMessage(Direction direction)
        {
            Header = Header.DIRECTION;
            Direction = direction;
        }

        public override string ToString() => $"{(int)Header}/{(int)Direction}";
    }
}
