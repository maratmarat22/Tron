using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Input;
using Tron.Common.Entities;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class JoinLobbyViewModel : BaseViewModel
    {
        // NAV & APP
        private readonly NavigationService _nav;
        private readonly App _app;

        // LOBBIES
        public DataGrid? LobbiesGrid { get; set; }

        // COMMANDS
        public ICommand FetchLobbiesCommand { get; }
        public ICommand JoinLobbyCommand { get; }
        public ICommand PasswordSubmitCommand { get; }
        public ICommand GoBackCommand { get; }

        // ELEMENTS
        public TextBox? PasswordTextBox { get; set; }

        private bool _passwordTextBoxVisibility;
        public bool PasswordTextBoxVisibility
        {
            get => _passwordTextBoxVisibility;
            set => SetProperty(ref _passwordTextBoxVisibility, value);
        }

        private Lobby? _currentPrivateLobby;

        // ERRORS
        private string _errorBlockText;
        public string ErrorBlockText
        {
            get => _errorBlockText;
            set => SetProperty(ref _errorBlockText, value);
        }

        private bool _errorBlockVisibility;
        public bool ErrorBlockVisibility
        {
            get => _errorBlockVisibility;
            set => SetProperty(ref _errorBlockVisibility, value);
        }

        internal JoinLobbyViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)System.Windows.Application.Current;

            FetchLobbiesCommand = new RelayCommand(OnFetchLobbies);
            JoinLobbyCommand = new RelayCommand(OnJoinLobby);
            PasswordSubmitCommand = new RelayCommand(OnPasswordSubmit);
            GoBackCommand = new RelayCommand(OnGoBack);

            _errorBlockText = string.Empty;
            _errorBlockVisibility = false;
        }

        private void OnFetchLobbies()
        {
            List<Lobby>? lobbies = _app.FetchLobbies();

            if (lobbies != null)
            {
                LobbiesGrid!.ItemsSource = new ObservableCollection<Lobby>(lobbies);
            }
        }

        private void OnJoinLobby(object? lobby)
        {
            Lobby l = (lobby as Lobby)!;

            if (l.IsPrivate)
            {
                _currentPrivateLobby = l;
                PasswordTextBoxVisibility = true;
            }
            else
            {
                TryJoinLobby(l.Master);
            }
        }

        private void OnPasswordSubmit()
        {
            string password = PasswordTextBox!.Text;

            if (password == _currentPrivateLobby!.Password)
            {
                TryJoinLobby(_currentPrivateLobby.Master);
            }
            else
            {
                ErrorBlockText = "WRONG PASSWORD";
                ErrorBlockVisibility = true;
            }
        }

        private async void TryJoinLobby(string master)
        {
            if (_app.JoinLobby(master))
            {
                _nav.Navigate(new AwaitingRoomPage(_nav, false));
            }
            else
            {
                ErrorBlockText = "CONNECTION ATTEMPT FAILED";
                ErrorBlockVisibility = true;
                await Task.Delay(TimeSpan.FromSeconds(2));
                ErrorBlockVisibility = false;
            }
        }

        private void OnGoBack() => _nav.Navigate(new MultiplayerMenuPage(_nav));
    }
}
