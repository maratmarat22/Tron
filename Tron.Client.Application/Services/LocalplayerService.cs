using System.Windows.Shapes;
using System.Windows.Controls;
using Tron.Client.Application.Models;
using System.Windows.Media;
using System.Windows;
using System.Windows.Navigation;
using Tron.Client.Application.Views;
using System.Numerics;

namespace Tron.Client.Application.Services
{
    internal class LocalplayerService : GameplayService
    {
        private int _restarts;

        internal LocalplayerService(NavigationService nav, Grid playersGrid, Grid arenaGrid, List<Player> players, Func<Task> CountDown, Action<Player> UpdatePlayerInfo)
            : base(nav, playersGrid, arenaGrid, CountDown, UpdatePlayerInfo)
        {
            _nav = nav;
            _restarts = 3;
            
            _playersGrid = playersGrid;
            _arenaGrid = arenaGrid;
            _players = players;
        }

        internal override async void Run()
        {
            await CountDown();
            GameTimer.Start();

            if (_restarts == 0)
            {
                GameTimer.Stop();
                _nav.Navigate(new MultiplayerMenuPage(_nav));
            }
        }

        protected override void GameTimer_Tick(object? sender, EventArgs e)
        {
            foreach (Player player in _players)
            {
                if (player.Alive)
                {
                    SetTrail(player);
                    Move(player);
                    CheckCrashes(player);
                    UpdatePlayerInfo(player);
                }
            }
        }

        protected override void Kill(Player player)
        {
            player.Alive = false;
            CleanTrail(player);
        }

        protected override void CheckWinners()
        {
            Player? winner = null;
            int alive = 0;

            foreach (Player player in _players)
            {
                if (player.Alive)
                {
                    winner = player;
                    ++alive;
                }
            }

            if (alive == 1)
            {
                ++winner!.Wins;
                if (--_restarts > 0)
                {
                    Restart();
                }
            }
            else if (alive == 0)
            {
                if (--_restarts > 0)
                {
                    Restart();
                }
            }
        }
    }
}
