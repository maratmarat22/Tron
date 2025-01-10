namespace Tron.Common.Entities
{
    public struct PlayerCoordinates
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public PlayerCoordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override bool Equals(object? obj)
        {
            if (obj is PlayerCoordinates other)
            {
                return Row == other.Row && Column == other.Column;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Column.GetHashCode();
        }
    }
}
