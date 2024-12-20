using System.Windows.Media;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Services;
using Tron.Common.Entities;
using Tron.Common.Messages;

namespace Tron.Client.Application.ViewModels.Game
{
    internal class LocalplayerViewModel : GameplayViewModel
    {
        internal LocalplayerViewModel(NavigationService nav) : base(nav)
        {
            _players.Add(new Player("PLAYER 1", new PlayerCoordinates(0, 0), Colors.Red, Direction.RIGHT));
            _players.Add(new Player("PLAYER 2", new PlayerCoordinates(0, 0), Colors.Blue, Direction.LEFT));
        }

        protected override void OnSetDirection(object? direction)
        {
            if (!_countdownTimer.IsEnabled)
            {
                _service!.SetDirection(_players[0], (Direction)direction!);
            }
        }

        protected override void OnExtraSetDirection(object? direction)
        {
            if (!_countdownTimer.IsEnabled)
            {
                _service!.SetDirection(_players[1], (Direction)direction!);
            }
        }

        protected override void OnInitGame()
        {
            AllocatePlayers();
            DisplayPlayerInfo();

            _service = new LocalplayerService(_nav, PlayersGrid!, ArenaGrid!, _players, CountDown, UpdatePlayerInfo);
            _service.Run();
        }

        protected override void AllocatePlayers()
        {
            _players[0].StartingCoordinates = new(ArenaGrid!.RowDefinitions.Count / 2, ArenaGrid.ColumnDefinitions.Count / 2 - 30);
            _players[1].StartingCoordinates = new(ArenaGrid.RowDefinitions.Count / 2, ArenaGrid.ColumnDefinitions.Count / 2 + 29);

            ArenaGrid.SetCoordinates(_players[0].Shape, _players[0].Coordinates);
            ArenaGrid.SetCoordinates(_players[1].Shape, _players[1].Coordinates);

            ArenaGrid.Children.Add(_players[0].Shape);
            ArenaGrid.Children.Add(_players[1].Shape);
        }

        protected override void OnGoBack()
        {
            _service!.GameTimer.Stop();
            _nav.GoBack();
        }
    }
}
