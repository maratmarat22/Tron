using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

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

        private bool _falseConnected;

        public bool NotConnected
        {
            get => _falseConnected;
            set => SetProperty(ref _falseConnected, value);
        }

        internal MultiplayerMenuViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)(System.Windows.Application.Current);

            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            JoinLobbyCommand = new RelayCommand(OnJoinLobby);
            InitLocalMpCommand = new RelayCommand(OnInitLocalMp);
            GoBackCommand = new RelayCommand(OnGoBack);

            NotConnected = false;
        }

        private void OnCreateLobby()
        {
            if (TryConnect())
            {
                _nav.Navigate(new CreateLobbyPage(_nav));
            }
        }

        private void OnJoinLobby()
        {
            if (TryConnect())
            {
                _nav.Navigate(new JoinLobbyPage(_nav));
            }
        }

        private void OnInitLocalMp()
        {
            _nav.Navigate(new ArenaPage(_nav, GameMode.LOCALPLAYER));
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }

        private bool TryConnect()
        {
            if (!string.IsNullOrWhiteSpace(_app.Username))
            {
                if (_app.TryLogIn())
                {
                    NotConnected = false;
                    return true;
                }
                else
                {
                    NotConnected = true;
                    return false;
                }
            }
            else
            {
                _nav.Navigate(new RegisterPage(_nav));
                return false;
            }
        }
    }
}
