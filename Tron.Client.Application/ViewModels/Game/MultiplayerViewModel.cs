using System.Windows.Media;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Services;
using Tron.Common.Entities;

namespace Tron.Client.Application.ViewModels.Game
{
    internal class MultiplayerViewModel : GameplayViewModel
    {
        private bool _enteredAsHost;
        
        internal MultiplayerViewModel(NavigationService nav, string hostName, string guestName, bool enteredAsHost) : base(nav)
        {
            _players.Add(new Player(hostName, new PlayerCoordinates(0, 0), Colors.Red, Direction.RIGHT));
            _players.Add(new Player(guestName, new PlayerCoordinates(0, 0), Colors.Blue, Direction.LEFT));

            _enteredAsHost = enteredAsHost;
        }

        protected override void OnInitGame()
        {
            AllocatePlayers();
            CreatePlayerData();

            _service = new MultiplayerService(_nav, _players, PlayerData!, Arena!, CountDown, UpdatePlayerData, DisplayWinner);
            _service.Run();
        }

        protected override void OnSetDirection(object? direction)
        {
            if (!_countdownTimer.IsEnabled)
            {
                if (_enteredAsHost)
                {
                    _service!.SetDirection(_players[0], (Direction)direction!);
                }
                else
                {
                    _service!.SetDirection(_players[1], (Direction)direction!);
                }
            }
        }

        protected override void OnExtraSetDirection(object? direction)
        {
            OnSetDirection(direction);
        }

        protected override void OnGoBack()
        {
            _service!.GameTimer.Stop();
            _nav.GoBack();
        }
    }
}
