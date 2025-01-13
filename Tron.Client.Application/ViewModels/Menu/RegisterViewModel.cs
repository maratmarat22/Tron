using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Input;
using Tron.Common.Messages;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class RegisterViewModel : BaseViewModel
    {
        // NAV & APP
        private readonly NavigationService _nav;
        private readonly App _app;

        // COMMANDS
        public ICommand GoBackCommand { get; }
        public ICommand RegisterCommand { get; }

        // ELEMENTS & ERRORS
        internal TextBox? RegisterTextBox { get; set; }

        private bool _tryAgainVisibility;
        public bool TryAgainVisibility
        {
            get => _tryAgainVisibility;
            set => SetProperty(ref _tryAgainVisibility, value);
        }

        internal RegisterViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)(System.Windows.Application.Current);

            GoBackCommand = new RelayCommand(OnGoBack);
            RegisterCommand = new RelayCommand(OnRegister);

            TryAgainVisibility = false;
        }

        private void OnRegister()
        {
            string username = RegisterTextBox!.Text;

            if (string.IsNullOrWhiteSpace(username) || username.Contains('/'))
            {
                TryAgainVisibility = true;
                return;
            }

            if (_app.Connect(Header.Register, username))
            {
                _app.Username = username;
                _app.WriteUsername();
                OnGoBack();
            }
            else
            {
                TryAgainVisibility = true;
            }
        }

        private void OnGoBack() => _nav.Navigate(new MultiplayerMenuPage(_nav));
    }
}
