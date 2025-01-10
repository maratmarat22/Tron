using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class MainMenuViewModel : BaseViewModel
    {
        private NavigationService _nav;

        public ICommand InitSpCommand { get; }

        public ICommand NavToMpMenuCommand { get; }

        public ICommand NavToSettingsCommand { get; }

        public ICommand ExitCommand { get; }

        internal MainMenuViewModel(NavigationService nav)
        {
            _nav = nav;

            InitSpCommand = new RelayCommand(OnInitSp);
            NavToMpMenuCommand = new RelayCommand(OnNavToMpMenu);
            NavToSettingsCommand = new RelayCommand(OnNavToSettings);
            ExitCommand = new RelayCommand(OnExit);
        }

        private void OnInitSp()
        {
            _nav.Navigate(new ArenaPage(_nav, GameMode.Singleplayer));
        }

        private void OnNavToMpMenu()
        {
            _nav.Navigate(new MultiplayerMenuPage(_nav));
        }

        private void OnNavToSettings()
        {
            _nav.Navigate(new SettingsPage(_nav));
        }

        private void OnExit()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
