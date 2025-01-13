namespace Tron.Common.Entities
{
    public struct PlayerCoordinates(int row, int column)
    {
        public int Row { get; set; } = row;

        public int Column { get; set; } = column;

        public override readonly bool Equals(object? obj)
        {
            if (obj is PlayerCoordinates other)
            {
                return Row == other.Row && Column == other.Column;
            }
            
            return false;
        }

        public override readonly int GetHashCode()
        {
            return Row.GetHashCode() ^ Column.GetHashCode();
        }

        public static bool operator ==(PlayerCoordinates left, PlayerCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerCoordinates left, PlayerCoordinates right)
        {
            return !(left == right);
        }
    }
}
