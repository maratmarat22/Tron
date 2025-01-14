using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using Tron.Common.Entities;

namespace Tron.Client.Application.Models
{
    internal class Player
    {
        public string Name { get; set; }

        public int Lives { get; set; }

        public int Score { get; set; }

        private PlayerCoordinates _initialCoordinates;

        public PlayerCoordinates InitialCoordinates
        {
            get => _initialCoordinates;
            set
            {
                _initialCoordinates = value;
                Coordinates = value;
            }
        }

        public PlayerCoordinates Coordinates { get; set; }

        public Color PlayerColor { get; private set; }

        public Rectangle Shape { get; private set; }

        public List<PlayerCoordinates> Trail { get; private set; }

        public Color TrailColor { get; private set; }

        public Direction InitialDirection { get; set; }

        public Direction Direction { get; set; }

        internal Player(string name, PlayerCoordinates coordinates, System.Windows.Media.Color color, Direction direction)
        {
            Name = name;
            Lives = (int)Constants.Lives;
            Score = 0;

            InitialCoordinates = coordinates;
            Coordinates = InitialCoordinates;
            
            PlayerColor = color;
            Shape = new Rectangle
            {
                Width = 5,
                Height = 5,
                Fill = new SolidColorBrush(PlayerColor),
            };

            Panel.SetZIndex(Shape, 2);

            Trail = [];
            TrailColor = Color.FromArgb((byte)(PlayerColor.A / 2), PlayerColor.R, PlayerColor.G, PlayerColor.B);

            InitialDirection = direction;
            Direction = InitialDirection;
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
