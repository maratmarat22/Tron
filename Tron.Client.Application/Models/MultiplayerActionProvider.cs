using Tron.Common.Entities;

namespace Tron.Client.Application.Models
{
    internal class MultiplayerActionProvider
    {
        private readonly App _app;

        private readonly Player[] _players;

        internal MultiplayerActionProvider(Player[] players)
        {
            _app = (App)(System.Windows.Application.Current);
            _players = players;
        }

        internal void FetchState(string[] changes)
        {
            var state = _app.RefreshSessionState(changes);

            _players[0].Coordinates = new(int.Parse(state!["HostX"]!), int.Parse(state!["HostY"]!));
            _players[1].Coordinates = new(int.Parse(state!["GuestX"]!), int.Parse(state!["GuestY"]!));

            _players[0].Direction = (Direction)Enum.Parse(typeof (Direction), (state!["HostDirection"]!));
            _players[1].Direction = (Direction)Enum.Parse(typeof(Direction), (state!["GuestDirection"]!));
        }

        internal void SetState()
        {
            string[] changes =
            [
                $"HostX:{_players[0].Coordinates.Row}",
                $"HostY:{_players[0].Coordinates.Column}",
                $"HostDirection:{_players[0].Direction}",
                $"GuestX:{_players[1].Coordinates.Row}",
                $"GuestY:{_players[1].Coordinates.Column}",
                $"GuestDirection:{_players[1].Direction}"
            ];

            FetchState(changes);
        }

        internal void FetchDirections()
        {
            Direction[] directions = _app.FetchDirections();

            _players[0].Direction = directions[0];
            _players[1].Direction = directions[1];
        }

        internal void SetDirection(string role, Direction direction)
        {
            _app.RefreshSessionState([role + "Direction:" + direction.ToString()]);
        }
    }
}
