using System.Windows.Navigation;
using Tron.Client.Application.Models;

namespace Tron.Client.Application.ViewModels.Game
{
    internal class SingleplayerViewModel : BaseViewModel
    {
        private NavigationService _nav;

        private int _playerCount;

        private IActionProvider _provider;

        internal SingleplayerViewModel(NavigationService nav)
        {
            _nav = nav;
            _playerCount = 2;
            _provider = new SpActionProvider();
        }


    }
}
