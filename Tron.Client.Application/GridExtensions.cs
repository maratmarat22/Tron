using System.Windows.Controls;
using System.Windows.Shapes;
using Tron.Common.Entities;

namespace Tron.Client.Application
{
    internal static class GridExtensions
    {
        public static void SetCoordinates(this Grid grid, Shape shape, PlayerCoordinates coordinates)
        {
            Grid.SetRow(shape, coordinates.Row);
            Grid.SetColumn(shape, coordinates.Column);
        }
    }
}
