using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Input;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class CreateLobbyViewModel : BaseViewModel
    {
        // NAV & APP
        private readonly NavigationService _nav;
        private readonly App _app;

        // COMMANDS
        public ICommand ChangePrivacyModeCommand { get; }
        public ICommand CreateLobbyCommand { get; }
        public ICommand GoBackCommand { get; }

        // ELEMENTS
        public TextBox? PasswordTextBox { get; set; }
        private bool _isPrivate;

        private string _privacyMode;
        public string PrivacyMode
        {
            get => _privacyMode;
            set
            {
                _isPrivate = value == "PRIVATE";
                SetProperty(ref _privacyMode, value);
            }
        }

        private bool _passwordTextBoxVisibility;
        public bool PasswordTextBoxVisibility
        {
            get => _passwordTextBoxVisibility;
            set
            {
                SetProperty(ref _passwordTextBoxVisibility, value);
            }
        }

        // ERRORS
        private bool _connectionAttemptFailed;
        public bool ConnectionAttemptFailed
        {
            get => _connectionAttemptFailed;
            set => SetProperty(ref _connectionAttemptFailed, value);
        }

        internal CreateLobbyViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)System.Windows.Application.Current;

            ChangePrivacyModeCommand = new RelayCommand(OnChangePrivacyMode);
            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            GoBackCommand = new RelayCommand(OnGoBack);

            _privacyMode = "PUBLIC";

            ConnectionAttemptFailed = false;
        }

        private void OnChangePrivacyMode()
        {
            if (PrivacyMode == "PUBLIC")
            {
                PrivacyMode = "PRIVATE";
                PasswordTextBoxVisibility = true;
            }
            else
            {
                PrivacyMode = "PUBLIC";
                PasswordTextBoxVisibility = false;
                PasswordTextBox!.Text = string.Empty;
            }
        }

        private async void OnCreateLobby()
        {
            string password = PasswordTextBox!.Text;

            if (_app.CreateLobby(_app.Username!, _isPrivate, password))
            {
                _nav.Navigate(new AwaitingRoomPage(_nav, true));
            }
            else
            {
                ConnectionAttemptFailed = true;
                await Task.Delay(TimeSpan.FromSeconds(2));
                ConnectionAttemptFailed = false;
                OnGoBack();
            }
        }

        private void OnGoBack() => _nav.Navigate(new MultiplayerMenuPage(_nav));
    }
}
