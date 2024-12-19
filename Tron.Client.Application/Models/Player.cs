using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Tron.Common.Messages;

namespace Tron.Client.Application.Models
{
    internal class Player
    {
        public Point Position { get; set; }
        
        public System.Windows.Shapes.Rectangle Shape { get; private set; }

        public System.Windows.Media.Color Color { get; private set; }

        public List<Point> Trail { get; private set; }

        public Direction Direction { get; set; }

        internal Player(Point position, System.Windows.Media.Color color, Direction direction)
        {
            Shape = new System.Windows.Shapes.Rectangle
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(color)
            };

            Trail = [];
            Direction = direction;
        }
    }
}
