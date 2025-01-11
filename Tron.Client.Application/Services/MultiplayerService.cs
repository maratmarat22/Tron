using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Common.Entities;
using Tron.Common.Networking;

namespace Tron.Client.Application.Services
{
    internal class MultiplayerService : GameplayService
    {
        MultiplayerActionProvider _provider;

        internal MultiplayerService(NavigationService nav, List<Player> players, Grid playerData, Grid arena, Func<Task> CountDown, Action UpdatePlayerData, Action<string?, Color> DisplayWinner)
            : base(nav, players, playerData, arena, CountDown, UpdatePlayerData, DisplayWinner)
        {
            _provider = new MultiplayerActionProvider(_players.ToArray());
        }

        internal override async void Run()
        {
            int deadCount = 0;
            Player? loser = null;

            foreach (Player player in _players)
            {
                if (player.Lives == 0)
                {
                    ++deadCount;
                    loser = player;
                }
            }

            if (deadCount > 0)
            {
                if (deadCount == 1)
                {
                    Player winner = _players.First(p => !p.Equals(loser));
                    DisplayWinner(winner.Name, winner.PlayerColor);
                }
                else
                {
                    DisplayWinner(null, Colors.Violet);
                }

                await Task.Delay(TimeSpan.FromSeconds(2));
                _nav.GoBack();
            }
            else
            {
                await CountDown();
                GameTimer.Start();
            }
        }

        protected override void GameTimer_Tick(object? sender, EventArgs e)
        {
            foreach (Player player in _players!)
            {
                if (GameTimer.IsEnabled)
                {
                    string[] changes =
                    [
                        $"HostX:{_players[0].Coordinates.Row}",
                        $"GuestX:{_players[1].Coordinates.Row}",
                        $"HostY:{_players[0].Coordinates.Column}",
                        $"GuestY:{_players[1].Coordinates.Column}",
                        $"HostDirection:{_players[0].Direction}",
                        $"GuestDirection:{_players[1].Direction}"
                    ];

                    _provider.FetchState(changes);

                    SetTrail(player);
                    Move(player);
                    CheckCollisions(player);
                }
            }
        }
    }
}
