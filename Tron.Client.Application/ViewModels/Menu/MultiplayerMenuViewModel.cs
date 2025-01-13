using System.Windows.Navigation;
using System.Windows.Input;
using Tron.Common.Messages;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class MultiplayerMenuViewModel : BaseViewModel
    {
        // NAV & APP
        private readonly NavigationService _nav;
        private readonly App _app;

        // COMMANDS
        public ICommand CreateLobbyCommand { get; }
        public ICommand JoinLobbyCommand { get; }
        public ICommand InitLocalplayerCommand { get; }
        public ICommand GoBackCommand { get; }

        // ERRORS
        private bool _connectionAttemptFailed;
        public bool ConnectionAttemptFailed
        {
            get => _connectionAttemptFailed;
            set => SetProperty(ref _connectionAttemptFailed, value);
        }

        internal MultiplayerMenuViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)System.Windows.Application.Current;

            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            JoinLobbyCommand = new RelayCommand(OnJoinLobby);
            InitLocalplayerCommand = new RelayCommand(OnInitLocalplayer);
            GoBackCommand = new RelayCommand(OnGoBack);

            ConnectionAttemptFailed = false;
        }

        private async Task<bool> Connect()
        {
            if (_app.CheckConnection()) return true;

            if (string.IsNullOrWhiteSpace(_app.Username))
            {
                _nav.Navigate(new RegisterPage(_nav));
                return false;
            }

            if (_app.Connect(Header.LogIn, _app.Username)) return true;

            ConnectionAttemptFailed = true;
            await Task.Delay(TimeSpan.FromSeconds(2));
            ConnectionAttemptFailed = false;

            return false;
        }

        private async void OnCreateLobby()
        {
            if (await Connect())
            {
                _nav.Navigate(new CreateLobbyPage(_nav));
            }
        }

        private async void OnJoinLobby()
        {
            if (await Connect())
            {
                _nav.Navigate(new JoinLobbyPage(_nav));
            }
        }

        private void OnInitLocalplayer() => _nav.Navigate(new ArenaPage(_nav, Mode.Localplayer));
        private void OnGoBack() => _nav.Navigate(new MainMenuPage(_nav));
    }
}
