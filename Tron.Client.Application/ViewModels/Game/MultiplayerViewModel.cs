using System.Windows.Navigation;

namespace Tron.Client.Application.ViewModels.Game
{
    internal class MultiplayerViewModel : BaseViewModel
    {
        private NavigationService _nav;

        internal MultiplayerViewModel(NavigationService nav)
        {
            _nav = nav;
        }
    }
}
