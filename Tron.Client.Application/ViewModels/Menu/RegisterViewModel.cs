using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Common.Messages;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class RegisterViewModel : BaseViewModel
    {
        private NavigationService _nav;

        private App _app;

        public ICommand GoBackCommand { get; }

        public ICommand RegisterCommand { get; }

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

        private void OnGoBack()
        {
            _nav.GoBack();
        }

        private void OnRegister()
        {
            string username = RegisterTextBox!.Text;

            if (!string.IsNullOrWhiteSpace(username) && !username.Contains('/'))
            {
                if (_app.TryConnect(Header.Register, username))
                {
                    _app.Username = username;
                    _app.LogUsername();
                    OnGoBack();
                }
                else
                {
                    TryAgainVisibility = true;
                }
            }
            else
            {
                TryAgainVisibility = true;
            }
        }
    }
}
