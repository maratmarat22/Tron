using Tron.Common.Entities;

namespace Tron.Client.Application.Models
{
    internal class SingleplayerActionProvider
    {
        private Player _player;
        private bool[,] _logicalArena;

        private Func<PlayerCoordinates, bool> OutBounds;
        private Action<Player, Direction> SetDirection;

        private Random _random;

        internal SingleplayerActionProvider(Player player, bool[,] logicalArena, Func<PlayerCoordinates, bool> OutBounds, Action<Player, Direction> SetDirection)
        {
            _player = player;
            _logicalArena = logicalArena;

            this.OutBounds = OutBounds;
            this.SetDirection = SetDirection;

            _random = new Random();
        }

        internal void GetDirection()
        {
            Direction[] directions = [Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT];

            List<Direction> possibleDirections = [];
            possibleDirections.Add(_player.Direction);

            foreach (Direction direction in directions)
            {
                PlayerCoordinates possibleCoordinates = direction switch
                {
                    Direction.UP => new PlayerCoordinates(_player.Coordinates.Row + 1, _player.Coordinates.Column),
                    Direction.DOWN => new PlayerCoordinates(_player.Coordinates.Row - 1, _player.Coordinates.Column),
                    Direction.LEFT => new PlayerCoordinates(_player.Coordinates.Row, _player.Coordinates.Column + 1),
                    Direction.RIGHT => new PlayerCoordinates(_player.Coordinates.Row, _player.Coordinates.Column - 1),
                    _ => _player.Coordinates
                };

                if (!BadMove(possibleCoordinates))
                {
                    possibleDirections.Add(direction);
                    break;
                }
            }

            SetDirection(_player, possibleDirections[_random.Next(possibleDirections.Count)]);
        }

        private bool BadMove(PlayerCoordinates coordinates)
        {
            return OutBounds(coordinates) || NotEmpty(coordinates);
        }

        private bool NotEmpty(PlayerCoordinates coordinates)
        {
            if (_logicalArena[coordinates.Row, coordinates.Column])
            {
                return true;
            }

            return false;
        }
    }
}
