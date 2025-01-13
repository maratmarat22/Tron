using System.Windows.Navigation;
using System.Windows.Media;
using Tron.Common.Entities;
using Tron.Client.Application.Services;
using Tron.Client.Application.Models;

namespace Tron.Client.Application.ViewModels.Game
{
    internal class SingleplayerViewModel : GameplayViewModel
    {
        internal SingleplayerViewModel(NavigationService nav) : base(nav)
        {
            _players.Add(new Player("PLAYER", new PlayerCoordinates(0, 0), Colors.Red, Direction.RIGHT));
            _players.Add(new Player("COMPUTER", new PlayerCoordinates(0, 0), Colors.Blue, Direction.LEFT));
        }

        protected override void OnInitGame()
        {
            AllocatePlayers();
            CreatePlayerData();

            _service = new SingleplayerService(_nav, _players, PlayerData!, Arena!, CountDown, UpdatePlayerData, DisplayWinner);
            _service.Run();
        }

        protected override void OnSetDirection(object? direction)
        {
            if (!_countdownTimer.IsEnabled)
            {
                _service!.SetDirection(_players[0]!, (Direction)direction!);
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
