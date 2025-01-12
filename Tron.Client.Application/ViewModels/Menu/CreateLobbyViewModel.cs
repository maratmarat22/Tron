using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Views;
using Tron.Common.Messages;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class CreateLobbyViewModel : BaseViewModel
    {
        private NavigationService _nav;

        private App _app;

        public TextBox? PasswordTextBox { get; set; }

        private bool _isPrivate;

        private string _privacyMode;

        public string PrivacyMode
        {
            get => _privacyMode;
            set
            {
                _isPrivate = value == "PRIVATE" ? true : false;
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

        private bool _connectionAttemptFailed;

        public bool ConnectionAttemptFailed
        {
            get => _connectionAttemptFailed;
            set => SetProperty(ref _connectionAttemptFailed, value);
        }

        public ICommand ChangePrivacyModeCommand { get; }

        public ICommand CreateLobbyCommand { get; }

        public ICommand GoBackCommand { get; }

        internal CreateLobbyViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)(System.Windows.Application.Current);

            _privacyMode = "PUBLIC";

            ConnectionAttemptFailed = false;

            ChangePrivacyModeCommand = new RelayCommand(OnChangePrivacyMode);
            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            GoBackCommand = new RelayCommand(OnGoBack);
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
                PasswordTextBox!.Text = null;
            }
        }

        private async void OnCreateLobby()
        {
            string password = PasswordTextBox!.Text;

            if (_app.TryCreateLobby(_app.Username!, _isPrivate, password))
            {
                _nav.Navigate(new AwaitingRoomPage(_nav, true));
            }
            else
            {
                ConnectionAttemptFailed = true;
                await Task.Delay(TimeSpan.FromSeconds(2));
                ConnectionAttemptFailed = false;
                _nav.GoBack();
            }
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
