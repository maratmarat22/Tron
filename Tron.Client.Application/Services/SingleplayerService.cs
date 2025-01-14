using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Media;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.Services
{
    internal class SingleplayerService : GameplayService
    {
        private readonly SingleplayerActionProvider _provider;
        
        internal SingleplayerService(NavigationService nav, List<Player> players, Grid playerData, Grid arena, Func<Task> CountDown, Action UpdatePlayerData, Action<string?, Color> DisplayWinner)
            : base(nav, players, playerData, arena, CountDown, UpdatePlayerData, DisplayWinner)
        {
            _provider = new(_players[1], _logicalArena, OutBounds, SetDirection);
        }

        internal override async void Run()
        {
            int losers = 0;
            Player? loser = null;

            foreach (Player player in _players)
            {
                if (player.Lives == 0)
                {
                    ++losers;
                    loser = player;
                }
            }

            if (losers > 0)
            {
                if (losers == 1)
                {
                    Player winner = _players.First(p => !p.Equals(loser));
                    DisplayWinner(winner.Name, winner.PlayerColor);
                }
                else
                {
                    DisplayWinner(null, Colors.Violet);
                }

                await Task.Delay(TimeSpan.FromSeconds(2));
                _nav.Navigate(new MainMenuPage(_nav));
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
