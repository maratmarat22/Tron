using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Controls;
using Tron.Common.Messages;

namespace Tron.Client.Application.Models
{
    internal class Player
    {
        public System.Windows.Shapes.Rectangle Shape { get; private set; }

        public System.Drawing.Color Color { get; private set; }

        public List<Point> Trail { get; private set; }

        public Direction Direction { get; set; }

        internal Player(Point start, System.Drawing.Color color)
        {
            Shape = new System.Windows.Shapes.Rectangle { Width = 10, Height = 10 };

            Canvas.SetLeft(Shape, start.X);
            Canvas.SetTop(Shape, start.Y);
            Trail = [start];
            Direction = Direction.Right;
        }
    }
}
