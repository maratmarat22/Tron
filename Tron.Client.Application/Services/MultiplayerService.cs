using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Networking;
using Tron.Common.Entities;
using Tron.Common.Messages;

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
                    _provider.FetchState();

                    SetTrail(player);
                    Move(player);
                    CheckCollisions(player);
                }
            }
        }

        internal override void SetDirection(Player player, Direction direction)
        {
            _provider.SetDirection(player, direction);
        }
    }
}
