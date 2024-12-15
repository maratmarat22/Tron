namespace Tron.Common.Messages
{
    public class PlayerCoordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PlayerCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
