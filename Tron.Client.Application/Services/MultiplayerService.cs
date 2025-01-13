using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Media;
using Tron.Common.Entities;
using Tron.Client.Application.Models;

namespace Tron.Client.Application.Services
{
    internal class MultiplayerService : GameplayService
    {
        private readonly MultiplayerActionProvider _provider;

        private readonly bool _enteredAsHost;

        private readonly App _app;

        internal MultiplayerService(NavigationService nav, List<Player> players, Grid playerData, Grid arena, Func<Task> CountDown, Action UpdatePlayerData, Action<string?, Color> DisplayWinner, bool enteredAsHost)
            : base(nav, players, playerData, arena, CountDown, UpdatePlayerData, DisplayWinner)
        {
            _provider = new MultiplayerActionProvider([.. _players]);
            _enteredAsHost = enteredAsHost;
            _app = (App)System.Windows.Application.Current;
        }

        internal override async void Run()
        {
            _provider.SetState();
            _provider.FetchState([]);

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
            int i = 0;
            foreach (Player player in _players!)
            {
                ++i;

                if (GameTimer.IsEnabled)
                {
                    if (i == 5)
                    {
                        _provider.FetchState([]);
                        i = 0;
                    }
                    else
                    {
                        _provider.FetchDirections();
                    }

                    SetTrail(player);
                    Move(player);
                    CheckCollisions(player);

                    _provider.SetState();
                }
            }
        }

        internal override void SetDirection(Player player, Direction direction)
        {
            base.SetDirection(player, direction);

            string role = _enteredAsHost ? "Host" : "Guest";

            _provider.SetDirection("Host", direction);
        }
    }
}
