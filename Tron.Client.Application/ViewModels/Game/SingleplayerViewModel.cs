using System.Windows.Navigation;
using Tron.Client.Application.Models;

namespace Tron.Client.Application.ViewModels.Game
{
    internal class SingleplayerViewModel : BaseViewModel
    {
        private NavigationService _nav;

        internal SingleplayerViewModel(NavigationService nav)
        {
            _nav = nav;
        }
    }
}
