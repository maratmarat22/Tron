using System.Drawing;
using System.Windows.Controls;
using System.Windows.Threading;
using Tron.Client.Application.Models;
using Tron.Common.Messages;

namespace Tron.Client.Application.Services
{
    internal class LocalGameplayService
    {
        private Canvas _arena;
        
        private List<Player> _players;

        private DispatcherTimer _timer;

        private List<Point> Trails;

        internal LocalGameplayService(Canvas arena, List<Player> players)
        {
            _arena = arena;
            _players = players;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Tick += Timer_Tick;

            Trails = [];
        }

        internal void Run()
        {
            _timer.Start();
        }

        internal void Timer_Tick(object? sender, EventArgs e)
        {
            foreach (Player player in _players)
            {
                MovePlayer(player);
            }
        }

        internal void MovePlayer(Player player)
        {
            player.Trail.Add(player.Position);
            Trails.Add(player.Position);

            player.Position = player.Direction switch
            {
                Direction.UP => new Point(player.Position.X, player.Position.Y - 5),
                Direction.DOWN => new Point(player.Position.X, player.Position.Y + 5),
                Direction.LEFT => new Point(player.Position.X - 5, player.Position.Y),
                Direction.RIGHT => new Point(player.Position.X + 5, player.Position.Y),
                _ => throw new NotImplementedException()
            };

            Canvas.SetLeft(player.Shape, player.Position.X);
            Canvas.SetTop(player.Shape, player.Position.Y);
        }

        internal void CheckCollisions(Player player)
        {
            if (player.Position.X < 0 || player.Position.Y < 0 || player.Position.X > _arena.Width || player.Position.Y > _arena.Height)
            {
                throw new NotImplementedException();
            }
            else
            {
                foreach (Player player1 in _players)
                {
                    if (player1.Trail.Contains(player.Position))
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }
    }
}
