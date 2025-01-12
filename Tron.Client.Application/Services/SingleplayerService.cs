using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Tron.Client.Application.Models;

namespace Tron.Client.Application.Services
{
    internal class SingleplayerService : GameplayService
    {
        private SingleplayerActionProvider _provider;
        
        internal SingleplayerService(NavigationService nav, List<Player> players, Grid playerData, Grid arena, Func<Task> CountDown, Action UpdatePlayerData, Action<string?, Color> DisplayWinner)
            : base(nav, players, playerData, arena, CountDown, UpdatePlayerData, DisplayWinner)
        {
            _provider = new(_players[1], _logicalArena, OutBounds, SetDirection);
        }

        internal override async void Run()
        {
            int deadCount = 0;
            Player? looser = null;

            foreach (Player player in _players)
            {
                if (player.Lives == 0)
                {
                    ++deadCount;
                    looser = player;
                }
            }

            if (deadCount > 0)
            {
                if (deadCount == 1)
                {
                    Player winner = _players.First(p => !p.Equals(looser));
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
                    if (player.Name == "COMPUTER")
                    {
                        SetDirection(_players[1], _provider.GetDirection());
                    }

                    SetTrail(player);
                    Move(player);
                    CheckCollisions(player);
                }
            }
        }
    }
}
