using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Tron.Common.Entities;

namespace Tron.Client.Application.Models
{
    internal class Player
    {
        public string Name { get; set; }

        public int Lives { get; set; }

        public int Score { get; set; }

        public bool Alive { get; set; }

        private PlayerCoordinates _startingCoordinates;

        public PlayerCoordinates StartingCoordinates
        {
            get => _startingCoordinates;
            set
            {
                _startingCoordinates = value;
                Coordinates = value;
            }
        }

        public PlayerCoordinates Coordinates { get; set; }

        public System.Windows.Media.Color PlayerColor { get; private set; }

        public Rectangle Shape { get; private set; }

        public List<PlayerCoordinates> Trail { get; private set; }

        public System.Windows.Media.Color TrailColor { get; private set; }

        public Direction StartingDirection { get; set; }

        public Direction Direction { get; set; }

        internal Player(string name, PlayerCoordinates coordinates, System.Windows.Media.Color color, Direction direction)
        {
            Name = name;
            Lives = (int)GameConstants.LIVES;
            Score = 0;

            StartingCoordinates = coordinates;
            Coordinates = StartingCoordinates;
            PlayerColor = color;

            Shape = new Rectangle
            {
                Width = 5,
                Height = 5,
                Fill = new SolidColorBrush(PlayerColor),
            };

            Panel.SetZIndex(Shape, 2);

            Trail = [];
            TrailColor = System.Windows.Media.Color.FromArgb((byte)(PlayerColor.A / 2), PlayerColor.R, PlayerColor.G, PlayerColor.B);

            StartingDirection = direction;
            Direction = StartingDirection;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Player other)
            {
                return PlayerColor == other.PlayerColor;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return PlayerColor.GetHashCode();
        }
    }
}
