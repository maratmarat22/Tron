using Tron.Common.Entities;

namespace Tron.Client.Application.Models
{
    internal class MultiplayerActionProvider
    {
        private readonly App _app;

        private readonly Player[] _players;

        internal MultiplayerActionProvider(Player[] players)
        {
            _app = (App)System.Windows.Application.Current;
            _players = players;
        }

        internal void FetchState(string[] changes)
        {
            var state = _app.RefreshSessionState(changes);

            int hostX = int.Parse(state["HostX"]);
            int hostY = int.Parse(state["HostY"]);
            
            int guestX = int.Parse(state["GuestX"]);
            int guestY = int.Parse(state["GuestY"]);
            
            Direction hostDirection = (Direction)Enum.Parse(typeof(Direction), (state["HostDirection"]));
            Direction guestDirection = (Direction)Enum.Parse(typeof(Direction), (state["GuestDirection"]));

            _players[0].Coordinates = new(hostX, hostY);
            _players[1].Coordinates = new(guestX, guestY);

            _players[0].Direction = hostDirection;
            _players[1].Direction = guestDirection;
        }

        internal void SetState()
        {
            string[] changes =
            [
                $"HostX:{_players[0].Coordinates.Row}",
                $"HostY:{_players[0].Coordinates.Column}",
                $"GuestX:{_players[1].Coordinates.Row}",
                $"GuestY:{_players[1].Coordinates.Column}",
                $"HostDirection:{_players[0].Direction}",
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
