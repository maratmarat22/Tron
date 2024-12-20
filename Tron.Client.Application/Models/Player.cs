using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using Tron.Common.Entities;
using Tron.Common.Messages;

namespace Tron.Client.Application.Models
{
    internal class Player
    {
        public PlayerCoordinates Coordinates { get; set; }

        public Rectangle Shape { get; private set; }

        public System.Windows.Media.Color Color { get; private set; }

        public List<PlayerCoordinates> Trail { get; private set; }

        public Direction Direction { get; set; }

        internal Player(PlayerCoordinates coordinates, System.Windows.Media.Color color, Direction direction)
        {
            Coordinates = coordinates;

            Shape = new Rectangle
            {
                Width = 5,
                Height = 5,
                Fill = new SolidColorBrush(color)
            };

            Color = color;
            Trail = [];
            Direction = direction;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Player other)
            {
                return Color == other.Color;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }
    }
}
