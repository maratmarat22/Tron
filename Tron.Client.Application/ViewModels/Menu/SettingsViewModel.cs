using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Abstract;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class SettingsViewModel : BaseViewModel
    {
        private NavigationService _nav;

        public ICommand GoBackCommand { get; }

        internal SettingsViewModel(NavigationService nav)
        {
            _nav = nav;
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
