using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;
using Tron.Client.Networking;
using Tron.Common.Messages;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class MultiplayerMenuViewModel : BaseViewModel
    {
        private NavigationService _nav;

        private App _app;

        public ICommand CreateLobbyCommand { get; }

        public ICommand JoinLobbyCommand { get; }

        public ICommand InitLocalMpCommand { get; }

        public ICommand GoBackCommand { get; }

        private bool _connectionAttemptFailed;

        public bool ConnectionAttemptFailed
        {
            get => _connectionAttemptFailed;
            set => SetProperty(ref _connectionAttemptFailed, value);
        }

        internal MultiplayerMenuViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)(System.Windows.Application.Current);

            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            JoinLobbyCommand = new RelayCommand(OnJoinLobby);
            InitLocalMpCommand = new RelayCommand(OnInitLocalMp);
            GoBackCommand = new RelayCommand(OnGoBack);

            ConnectionAttemptFailed = false;
        }

        private async void OnCreateLobby()
        {
            if (await TryConnect())
            {
                _nav.Navigate(new CreateLobbyPage(_nav));
            }
        }

        private async void OnJoinLobby()
        {
            if (await TryConnect())
            {
                _nav.Navigate(new JoinLobbyPage(_nav));
            }
        }

        private void OnInitLocalMp()
        {
            _nav.Navigate(new ArenaPage(_nav, GameMode.Localplayer));
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }

        private async Task<bool> TryConnect()
        {
            if (_app.AckRequest(new Message(Header.ConnectionCheck, []), Point.Server))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(_app.Username))
            {
                _nav.Navigate(new RegisterPage(_nav));
                
                return false;
            }

            if (_app.TryConnect(new Message(Header.LogIn, [_app.Username])))
            {
                return true;
            }

            ConnectionAttemptFailed = true;
            await Task.Delay(TimeSpan.FromSeconds(2));
            ConnectionAttemptFailed = false;

            return false;
        }
    }
}
