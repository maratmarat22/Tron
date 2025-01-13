using System.Windows.Navigation;
using System.Windows.Input;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class MainMenuViewModel : BaseViewModel
    {
        // NAV
        private readonly NavigationService _nav;

        // COMMANDS
        public ICommand InitSingleplayerCommand { get; }
        public ICommand NavToMultiplayerMenuCommand { get; }
        public ICommand NavToSettingsCommand { get; }
        public ICommand ExitCommand { get; }

        internal MainMenuViewModel(NavigationService nav)
        {
            _nav = nav;

            InitSingleplayerCommand = new RelayCommand(OnInitSingleplayer);
            NavToMultiplayerMenuCommand = new RelayCommand(OnNavToMultiplayerMenu);
            NavToSettingsCommand = new RelayCommand(OnNavToSettings);
            ExitCommand = new RelayCommand(OnExit);
        }

        private void OnInitSingleplayer() => _nav.Navigate(new ArenaPage(_nav, Mode.Singleplayer));
        private void OnNavToMultiplayerMenu() => _nav.Navigate(new MultiplayerMenuPage(_nav));
        private void OnNavToSettings() => _nav.Navigate(new SettingsPage(_nav));
        private void OnExit() => System.Windows.Application.Current.Shutdown();
    }
}
