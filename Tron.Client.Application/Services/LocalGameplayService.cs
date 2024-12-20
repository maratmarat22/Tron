using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Threading;
using Tron.Client.Application.Models;
using Tron.Common.Entities;
using Tron.Common.Messages;
using System.Windows.Media;

namespace Tron.Client.Application.Services
{
    internal class LocalGameplayService
    {
        private Grid _arenaGrid;
        
        private List<Player> _players;

        private DispatcherTimer _timer;

        private Dictionary<Player, DateTime> _lastDirectionChange;

        internal LocalGameplayService(Grid arenaGrid, List<Player> players)
        {
            _arenaGrid = arenaGrid;
            _players = players;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(25);
            _timer.Tick += Timer_Tick;

            _lastDirectionChange = [];
        }

        internal void Run()
        {
            _timer.Start();
        }

        internal void Timer_Tick(object? sender, EventArgs e)
        {
            foreach (Player player in _players)
            {
                AddTrail(player);
                MovePlayer(player);
                CheckCollisions(player);
            }
        }

        internal void AddTrail(Player player)
        {
            player.Trail.Add(player.Coordinates);

            Rectangle trail = new()
            {
                Width = 5,
                Height = 5,
                Fill = new SolidColorBrush(player.Color)
            };

            Panel.SetZIndex(trail, 1);

            _arenaGrid.SetCoordinates(trail, player.Coordinates);
            _arenaGrid.Children.Add(trail);
        }

        internal void MovePlayer(Player player)
        {
            player.Coordinates = player.Direction switch
            {
                Direction.UP => new PlayerCoordinates(player.Coordinates.Row - 1, player.Coordinates.Column),
                Direction.DOWN => new PlayerCoordinates(player.Coordinates.Row + 1, player.Coordinates.Column),
                Direction.LEFT => new PlayerCoordinates(player.Coordinates.Row, player.Coordinates.Column - 1),
                Direction.RIGHT => new PlayerCoordinates(player.Coordinates.Row, player.Coordinates.Column + 1),
                _ => player.Coordinates
            };

            if (InBounds(player))
            {
                Kill(player);
            }
            else _arenaGrid.SetCoordinates(player.Shape, player.Coordinates);
        }

        internal void CheckCollisions(Player player)
        {
            foreach (Player other in _players)
            {
                if (OnTrail(player, other))
                {
                    Kill(player);
                }
                if (Crash(player, other))
                {
                    Kill(player);
                    Kill(other);
                }
            }
        }

        internal void SetDirection(Player player, Direction direction)
        {
            if (_lastDirectionChange.TryGetValue(player, out DateTime value))
            {
                TimeSpan timeSinceLastChange = DateTime.Now - value;

                if (timeSinceLastChange < TimeSpan.FromMilliseconds(10)) return;
            }

            player.Direction = direction switch
            {
                Direction.UP when player.Direction != Direction.DOWN => Direction.UP,
                Direction.DOWN when player.Direction != Direction.UP => Direction.DOWN,
                Direction.LEFT when player.Direction != Direction.RIGHT => Direction.LEFT,
                Direction.RIGHT when player.Direction != Direction.LEFT => Direction.RIGHT,
                _ => player.Direction
            };

            _lastDirectionChange[player] = DateTime.Now;
        }

        internal void Kill(Player player)
        {
            Environment.Exit(0);
        }

        private bool InBounds(Player player)
        {
            return player.Coordinates.Row < 0 || player.Coordinates.Column < 0 ||
                   player.Coordinates.Row > _arenaGrid.RowDefinitions.Count ||
                   player.Coordinates.Column > _arenaGrid.ColumnDefinitions.Count;
        }

        private bool OnTrail(Player player1, Player player2)
        {
            return player2.Trail.Contains(player1.Coordinates) && !player1.Coordinates.Equals(player1.Trail.Last());
        }

        private bool Crash(Player player1, Player player2)
        {
            return player1.Coordinates.Equals(player2.Coordinates) && !player1.Equals(player2);
        }
    }
}
