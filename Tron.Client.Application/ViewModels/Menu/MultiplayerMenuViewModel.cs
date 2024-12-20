using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class MultiplayerMenuViewModel : BaseViewModel
    {
        private NavigationService _nav;

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

            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            JoinLobbyCommand = new RelayCommand(OnJoinLobby);
            InitLocalMpCommand = new RelayCommand(OnInitLocalMp);
            GoBackCommand = new RelayCommand(OnGoBack);

            ConnectionAttemptFailed = false;
        }

        private void OnCreateLobby()
        {
            if (((App)System.Windows.Application.Current).TryConnectToServer())
            {
                ConnectionAttemptFailed = false;
                _nav.Navigate(new CreateLobbyPage(_nav));
            }
            else ConnectionAttemptFailed = true;
        }

        private void OnJoinLobby()
        {
            if (((App)System.Windows.Application.Current).TryConnectToServer())
            {
                ConnectionAttemptFailed = false;
                _nav.Navigate(new JoinLobbyPage(_nav));
            }
            else ConnectionAttemptFailed = true;
        }

        private void OnInitLocalMp()
        {
            _nav.Navigate(new ArenaPage(_nav, 2, GameMode.LOCALPLAYER));
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
