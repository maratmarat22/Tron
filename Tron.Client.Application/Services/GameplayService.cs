using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tron.Client.Application.Models;
using Tron.Common.Entities;

namespace Tron.Client.Application.Services
{
    internal abstract class GameplayService
    {
        protected NavigationService _nav;

        protected List<Player> _players;

        protected Grid _playerData;

        protected Grid _arena;

        protected bool[,] _logicalArena;

        public DispatcherTimer GameTimer { get; }

        protected readonly Dictionary<Player, DateTime> _lastDirectionChange;

        protected Func<Task> CountDown;

        protected Action UpdatePlayerData;

        protected Action<string?, Color> DisplayWinner;

        internal GameplayService(NavigationService nav, List<Player> players, Grid playerData, Grid arena, Func<Task> CountDown, Action UpdatePlayerData, Action<string?, Color> DisplayWinner)
        {
            _nav = nav;

            _players = players;

            _playerData = playerData;
            _arena = arena;
            
            _logicalArena = new bool[_arena.RowDefinitions.Count, _arena.ColumnDefinitions.Count];
            
            foreach (Player player in _players)
            {
                _logicalArena[player.Coordinates.Row, player.Coordinates.Column] = true;
            }

            GameTimer = new DispatcherTimer();
            GameTimer.Interval = TimeSpan.FromMilliseconds((int)GameConstants.GAME_TICK);
            GameTimer.Tick += GameTimer_Tick;

            _lastDirectionChange = [];

            this.CountDown = CountDown;
            this.UpdatePlayerData = UpdatePlayerData;
            this.DisplayWinner = DisplayWinner;
        }

        internal abstract void Run();

        protected abstract void GameTimer_Tick(object? sender, EventArgs e);

        protected void SetTrail(Player player)
        {
            player.Trail.Add(player.Coordinates);

            Rectangle trail = new()
            {
                Width = player.Shape.Width,
                Height = player.Shape.Width,
                Fill = new SolidColorBrush(player.TrailColor)
            };

            Panel.SetZIndex(trail, 1);

            _arena.SetCoordinates(trail, player.Coordinates);
            _arena.Children.Add(trail);
            _logicalArena[player.Coordinates.Row, player.Coordinates.Column] = true;
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

            if (OutBounds(player.Coordinates))
            {
                Player other = _players!.First(p => !p.Equals(player));
                --player.Lives;
                other.Score += (int)GameConstants.ALIVE_SCORE;
                Restart();
            }
            else
            {
                _arena.SetCoordinates(player.Shape, player.Coordinates);
                _logicalArena[player.Coordinates.Row, player.Coordinates.Column] = true;
            }
        }

        protected void CheckCollisions(Player player)
        {
            foreach (Player other in _players)
            {
                if (Accident(player, other))
                {
                    --player.Lives;
                    --other.Lives;
                    Restart();
                    break;
                }
                else if (OnTrail(player, other))
                {
                    --player.Lives;
                    
                    if (player.Equals(other))
                    {
                        Player alive = _players.First(p => !p.Equals(player));
                        alive.Score += (int)GameConstants.ALIVE_SCORE;
                    }
                    else
                    {
                        other.Score += (int)GameConstants.KILL_SCORE;
                    }

                    Restart();
                    break;
                }
            }
        }

        internal virtual void SetDirection(Player player, Direction direction)
        {
            if (MoveCooldownPassed(player))
            {
                player.Direction = direction switch
                {
                    Direction.UP when player.Direction != Direction.DOWN => Direction.UP,
                    Direction.DOWN when player.Direction != Direction.UP => Direction.DOWN,
                    Direction.LEFT when player.Direction != Direction.RIGHT => Direction.LEFT,
                    Direction.RIGHT when player.Direction != Direction.LEFT => Direction.RIGHT,
                    _ => player.Direction
                };
            }

            _lastDirectionChange[player] = DateTime.Now;
        }

        private bool MoveCooldownPassed(Player player)
        {
            if (_lastDirectionChange.TryGetValue(player, out DateTime value))
            {
                TimeSpan timeSinceLastChange = DateTime.Now - value;

                return (timeSinceLastChange > TimeSpan.FromMilliseconds((int)GameConstants.MOVE_COOLDOWN));
            }

            return true;
        }

        protected bool OutBounds(PlayerCoordinates coordinates)
        {
            return coordinates.Row < 0 || coordinates.Column < 0 ||
                   coordinates.Row >= _arena.RowDefinitions.Count || coordinates.Column >= _arena.ColumnDefinitions.Count;
        }

        protected bool OnTrail(Player victim, Player killer)
        {
            return killer.Trail.Contains(victim.Coordinates) && !victim.Coordinates.Equals(victim.Trail.Last());
        }

        protected bool Accident(Player player1, Player player2)
        {
            return player1.Coordinates.Equals(player2.Coordinates) && !player1.Equals(player2);
        }

        protected async void Restart()
        {
            GameTimer.Stop();

            UpdatePlayerData();
            await Task.Delay(TimeSpan.FromSeconds(1));

            Canvas Net = _arena.Children.OfType<Canvas>().FirstOrDefault(c => c.Name == "Net")!;
            _arena.Children.Clear();
            _arena.Children.Add(Net);
            _logicalArena = new bool[_arena.RowDefinitions.Count, _arena.ColumnDefinitions.Count];

            foreach (Player player in _players!)
            {
                player.Alive = true;
                player.Trail.Clear();
                player.Coordinates = player.StartingCoordinates;
                player.Direction = player.StartingDirection;

                _arena.SetCoordinates(player.Shape, player.Coordinates);
                _arena.Children.Add(player.Shape);
            }

            Run();
        }
    }
}
