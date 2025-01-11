using System.Text.Json;
using Tron.Client.Networking;
using Tron.Common.Entities;
using Tron.Common.Messages;

namespace Tron.Client.Application.Models
{
    internal class MultiplayerActionProvider
    {
        private App _app;

        private Player[] _players;

        internal MultiplayerActionProvider(Player[] players)
        {
            _app = (App)(System.Windows.Application.Current);
            _players = players;
        }

        internal void FetchState(string[] changes)
        {
            string[] payload;
            
            if (changes.Length == 0)
            {
                payload = (_app.PayloadRequest(new Message(Header.SessionState, []), Point.Master))!;
            }
            else
            {
                payload = (_app.PayloadRequest(new Message(Header.SessionState, [string.Join(',', changes)]), Point.Master))!;
            }
            
            var state = JsonSerializer.Deserialize<Dictionary<string, string?>>(payload[0]);

            _players[0].Coordinates = new(int.Parse(state!["HostX"]!), int.Parse(state!["HostY"]!));
            _players[1].Coordinates = new(int.Parse(state!["GuestX"]!), int.Parse(state!["GuestY"]!));

            _players[0].Direction = (Direction)Enum.Parse(typeof (Direction), (state!["HostDirection"]!));
            _players[1].Direction = (Direction)Enum.Parse(typeof(Direction), (state!["GuestDirection"]!));
        }

        internal void FetchDirections()
        {
            string[] payload = _app.PayloadRequest(new Message(Header.FetchDirections, []), Point.Master)!;

            _players[0].Direction = (Direction)Enum.Parse(typeof(Direction), payload[0]);
            _players[1].Direction = (Direction)Enum.Parse(typeof(Direction), payload[1]);
        }
    }
}
