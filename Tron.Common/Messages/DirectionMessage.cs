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
            Header = Header.Direction;
            Direction = direction;
        }

        public override string ToString() => Header.ToString() + '/' + Direction.ToString();
    }
}
