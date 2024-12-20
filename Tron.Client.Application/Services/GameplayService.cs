using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tron.Client.Application.Models;
using Tron.Common.Entities;
using Tron.Common.Messages;

namespace Tron.Client.Application.Services
{
    internal abstract class GameplayService
    {
        protected NavigationService _nav;

        protected Grid _playersGrid;

        protected Grid _arenaGrid;

        public DispatcherTimer GameTimer { get; }

        protected readonly Dictionary<Player, DateTime> _lastDirectionChange;

        protected List<Player> _players;

        protected Func<Task> CountDown;

        protected Action<Player> UpdatePlayerInfo;

        internal GameplayService(NavigationService nav, Grid playersGrid, Grid arenaGrid, Func<Task> CountDown, Action<Player> UpdatePlayerInfo)
        {
            _nav = nav;
            _playersGrid = playersGrid;
            _arenaGrid = arenaGrid;

            GameTimer = new DispatcherTimer();
            GameTimer = new DispatcherTimer();
            GameTimer.Interval = TimeSpan.FromMilliseconds(25);
            GameTimer.Tick += GameTimer_Tick;

            _lastDirectionChange = [];

            _players = [];

            this.CountDown = CountDown;
            this.UpdatePlayerInfo = UpdatePlayerInfo;
        }

        internal abstract void Run();

        protected abstract void CheckWinners();

        protected abstract void GameTimer_Tick(object? sender, EventArgs e);

        protected void SetTrail(Player player)
        {
            player.Trail.Add(player.Coordinates);

            Rectangle trail = new()
            {
                Width = 5,
                Height = 5,
                Fill = new SolidColorBrush(player.TrailColor)
            };

            Panel.SetZIndex(trail, 1);

            _arenaGrid.SetCoordinates(trail, player.Coordinates);
            _arenaGrid.Children.Add(trail);
        }

        protected void Move(Player player)
        {
            player.Coordinates = player.Direction switch
            {
                Direction.UP => new PlayerCoordinates(player.Coordinates.Row - 1, player.Coordinates.Column),
                Direction.DOWN => new PlayerCoordinates(player.Coordinates.Row + 1, player.Coordinates.Column),
                Direction.LEFT => new PlayerCoordinates(player.Coordinates.Row, player.Coordinates.Column - 1),
                Direction.RIGHT => new PlayerCoordinates(player.Coordinates.Row, player.Coordinates.Column + 1),
                _ => player.Coordinates
            };

            if (OutOfBounds(player))
            {
                Kill(player);
            }
            else _arenaGrid.SetCoordinates(player.Shape, player.Coordinates);
        }

        protected void CheckCrashes(Player player)
        {
            foreach (Player other in _players)
            {
                if (OnTrail(player, other))
                {
                    Kill(player);
                    
                    if (!other.Equals(player))
                    {
                        other.Score += 200;
                    }

                    CheckWinners();
                    break;
                }
                if (Crash(player, other))
                {
                    Kill(player);
                    Kill(other);
                    CheckWinners();
                    break;
                }
            }
        }

        internal void SetDirection(Player player, Direction direction)
        {
            if (_lastDirectionChange.TryGetValue(player, out DateTime value))
            {
                TimeSpan timeSinceLastChange = DateTime.Now - value;

                if (timeSinceLastChange < TimeSpan.FromMilliseconds(15)) return;
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

        protected abstract void Kill(Player player);

        protected bool OutOfBounds(Player player)
        {
            return player.Coordinates.Row < 0 || player.Coordinates.Column < 0 ||
                   player.Coordinates.Row > _arenaGrid.RowDefinitions.Count ||
                   player.Coordinates.Column > _arenaGrid.ColumnDefinitions.Count;
        }

        protected bool OnTrail(Player player, Player trailOwner)
        {
            return trailOwner.Trail.Contains(player.Coordinates) && !player.Coordinates.Equals(player.Trail.Last());
        }

        protected bool Crash(Player player1, Player player2)
        {
            return player1.Coordinates.Equals(player2.Coordinates) && !player1.Equals(player2);
        }

        protected void CleanTrail(Player player)
        {
            List<Rectangle> trailsToRemove = _arenaGrid.Children.OfType<Rectangle>()
                .Where(trail => ((SolidColorBrush)trail.Fill).Color == player.TrailColor).ToList();

            foreach (Rectangle trail in trailsToRemove)
            {
                _arenaGrid.Children.Remove(trail);
            }

            _arenaGrid.Children.Remove(player.Shape);
        }

        protected async void Restart()
        {
            GameTimer.Stop();

            await Task.Delay(TimeSpan.FromSeconds(1));

            Canvas Net = _arenaGrid.Children.OfType<Canvas>().FirstOrDefault(c => c.Name == "Net")!;
            _arenaGrid.Children.Clear();
            _arenaGrid.Children.Add(Net);

            foreach (Player player in _players)
            {
                player.Alive = true;
                player.Trail.Clear();
                player.Coordinates = player.StartingCoordinates;
                player.Direction = player.StartingDirection;
            }

            foreach (var player in _players)
            {
                _arenaGrid.SetCoordinates(player.Shape, player.Coordinates);
                _arenaGrid.Children.Add(player.Shape);
            }

            Run();
        }
    }
}
