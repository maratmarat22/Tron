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

        private bool _enteredAsHost;

        private App _app;

        internal MultiplayerService(NavigationService nav, List<Player> players, Grid playerData, Grid arena, Func<Task> CountDown, Action UpdatePlayerData, Action<string?, Color> DisplayWinner, bool enteredAsHost)
            : base(nav, players, playerData, arena, CountDown, UpdatePlayerData, DisplayWinner)
        {
            _provider = new MultiplayerActionProvider(_players.ToArray());
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

            if (_enteredAsHost)
            {
                _app.PayloadRequest(new Message(Header.SessionState, [$"HostDirection:{player.Direction}"]), Point.Master);
            }
            else
            {
                _app.PayloadRequest(new Message(Header.SessionState, [$"GuestDirection:{player.Direction}"]), Point.Master);
            }
        }
    }
}
