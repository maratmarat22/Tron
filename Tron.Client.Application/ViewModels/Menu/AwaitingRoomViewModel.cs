using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class AwaitingRoomViewModel : BaseViewModel
    {
        private readonly NavigationService _nav;
        private bool _isHost;
        private readonly App _app;

        // NAMES
        private string _hostname;

        public string Hostname
        {
            get => _hostname;
            set => SetProperty(ref _hostname, value);
        }

        private string _guestname;

        public string Guestname
        {
            get => _guestname;
            set => SetProperty(ref _guestname, value);
        }


        // READY CHARS
        private char _hostReadyChar;

        public char HostReadyChar
        {
            get => _hostReadyChar;
            set
            {
                HostReadyCharColor = value == 'R' ? Colors.LightGreen : Colors.HotPink;
                SetProperty(ref _hostReadyChar, value);
            }
        }

        private char _guestReadyChar;

        public char GuestReadyChar
        {
            get => _guestReadyChar;
            set
            {
                GuestReadyCharColor = value == 'R' ? Colors.LightGreen : Colors.HotPink;
                SetProperty(ref _guestReadyChar, value);
            }
        }


        // READY CHAR COLORS
        private Color _hostReadyCharColor;

        public Color HostReadyCharColor
        {
            get => _hostReadyCharColor;
            set => SetProperty(ref _hostReadyCharColor, value);
        }

        private Color _guestReadyCharColor;

        public Color GuestReadyCharColor
        {
            get => _guestReadyCharColor;
            set => SetProperty(ref _guestReadyCharColor, value);
        }


        // OTHER
        private bool _startButtonVisibility;

        public bool StartButtonVisibility
        {
            get => _startButtonVisibility;
            set => SetProperty(ref _startButtonVisibility, value);
        }

        public ICommand ReadyCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand GoBackCommand { get; }

        internal AwaitingRoomViewModel(NavigationService nav, bool isHost)
        {
            _nav = nav;
            _isHost = isHost;
            _app = ((App)(System.Windows.Application.Current));

            if (_isHost)
            {
                Hostname = _app.Username!;
            }
            else
            {
                Guestname = _app.Username!;
            }

            HostReadyChar = 'N';
            GuestReadyChar = 'N';

            StartButtonVisibility = false;

            ReadyCommand = new RelayCommand(OnReady);
            StartCommand = new RelayCommand(OnStart);
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        private void OnReady()
        {
            if (_isHost)
            {
                HostReadyChar = 'R';
            }
            else
            {
                GuestReadyChar = 'R';
            }

            if (HostReadyChar == 'R' && GuestReadyChar == 'R')
            {
                StartButtonVisibility = true;
            }
        }

        private void OnStart()
        {
            _nav.Navigate(new ArenaPage(_nav, GameMode.MULTIPLAYER));
        }

        private void OnGoBack()
        {
            //_app.DeleteLobby();
            _nav.GoBack();
        }
    }
}
