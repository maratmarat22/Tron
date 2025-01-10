using System.Net;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Views;
using Tron.Common.Entities;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Client.Networking;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class JoinLobbyViewModel : BaseViewModel
    {
        private readonly NavigationService _nav;

        private readonly App _app;

        public DataGrid? LobbiesGrid { get; set; }

        public ObservableCollection<Lobby> Lobbies { get; set; }

        public ICommand FetchLobbiesCommand { get; set; }

        public ICommand JoinLobbyCommand { get; set; }

        public ICommand PasswordSubmitCommand { get; set; }

        public ICommand GoBackCommand { get; }

        public TextBox? PasswordTextBox { get; set; }

        private bool _passwordTextBoxVisibility;
        
        public bool PasswordTextBoxVisibility
        {
            get => _passwordTextBoxVisibility;
            set => SetProperty(ref _passwordTextBoxVisibility, value);
        }

        private Lobby? _currentPrivateLobby;

        private bool _errorBlockVisibility;

        public bool ErrorBlockVisibility
        {
            get => _errorBlockVisibility;
            set => SetProperty(ref _errorBlockVisibility, value);
        }

        private string _errorBlockText;

        public string ErrorBlockText
        {
            get => _errorBlockText;
            set => SetProperty(ref _errorBlockText, value);
        }

        internal JoinLobbyViewModel(NavigationService nav)
        {
            _nav = nav;
            FetchLobbiesCommand = new RelayCommand(OnFetchLobbies);
            JoinLobbyCommand = new RelayCommand(OnJoinLobby);
            PasswordSubmitCommand = new RelayCommand(OnPasswordSubmit);
            GoBackCommand = new RelayCommand(OnGoBack);
            Lobbies = [];

            _app = (App)System.Windows.Application.Current;

            _errorBlockVisibility = false;
            _errorBlockText = string.Empty;
        }

        private void OnFetchLobbies()
        {
            string[]? payload = _app.PayloadRequest(new Message(Header.FetchLobbies, []), Point.Server);

            if (payload != null)
            {
                Lobbies = new ObservableCollection<Lobby>(JsonSerializer.Deserialize<List<Lobby>>(payload[1])!);
                LobbiesGrid!.ItemsSource = Lobbies;
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
                TryJoinLobby(l);
            }
        }

        private void OnPasswordSubmit()
        {
            string password = PasswordTextBox!.Text;

            if (password == _currentPrivateLobby!.Password)
            {
                TryJoinLobby(_currentPrivateLobby);
            }
            else
            {
                ErrorBlockText = "WRONG PASSWORD";
                ErrorBlockVisibility = true;
            }
        }

        private async void TryJoinLobby(Lobby lobby)
        {
            if (_app.TryJoinLobby(new Message(Header.JoinLobby, ["", lobby.Master])))
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

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
