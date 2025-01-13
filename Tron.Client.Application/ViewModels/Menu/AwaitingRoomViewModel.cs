using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class AwaitingRoomViewModel : BaseViewModel
    {
        private readonly NavigationService _nav;
        private readonly bool _enteredAsHost;
        private readonly App _app;
        private Dictionary<string, string?> _state;

        // NAMES
        private string? _hostName;

        public string? HostName
        {
            get => _hostName;
            set => SetProperty(ref _hostName, value);
        }

        private string? _guestName;

        public string? GuestName
        {
            get => _guestName;
            set => SetProperty(ref _guestName, value);
        }

        private bool _hostReady;

        internal bool HostReady
        {
            get => _hostReady;
            set
            {
                _hostReady = value;

                if (_hostReady)
                {
                    HostReadyChar = 'R';
                    HostReadyCharColor = Colors.LightGreen;

                    if (_enteredAsHost)
                    {
                        ReadyButtonText = "NOT READY";
                        ReadyButtonColor = Colors.HotPink;
                    }
                }
                else
                {
                    HostReadyChar = 'N';
                    HostReadyCharColor = Colors.HotPink;

                    if (_enteredAsHost)
                    {
                        ReadyButtonText = "READY";
                        ReadyButtonColor = Colors.LightGreen;
                    }
                }
            }
        }

        private bool _guestReady;

        internal bool GuestReady
        {
            get => _guestReady;
            set
            {
                _guestReady = value;

                if (_guestReady)
                {
                    GuestReadyChar = 'R';
                    GuestReadyCharColor = Colors.LightGreen;

                    if (!_enteredAsHost)
                    {
                        ReadyButtonText = "NOT READY";
                        ReadyButtonColor = Colors.HotPink;
                    }
                }
                else
                {
                    GuestReadyChar = 'N';
                    GuestReadyCharColor = Colors.HotPink;

                    if (!_enteredAsHost)
                    {
                        ReadyButtonText = "READY";
                        ReadyButtonColor = Colors.LightGreen;
                    }
                }
            }
        }

        // READY CHARS
        private char _hostReadyChar;

        public char HostReadyChar
        {
            get => _hostReadyChar;
            set => SetProperty(ref _hostReadyChar, value);
        }

        private char _guestReadyChar;

        public char GuestReadyChar
        {
            get => _guestReadyChar;
            set => SetProperty(ref _guestReadyChar, value);
        }

        private bool _guestReadyCharVisibility;
        
        public bool GuestReadyCharVisibility
        {
            get => _guestReadyCharVisibility;
            set => SetProperty(ref _guestReadyCharVisibility, value);
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

        private string? _readyButtonText;

        public string? ReadyButtonText
        {
            get => _readyButtonText;
            set => SetProperty(ref _readyButtonText, value);
        }

        private Color _readyButtonColor;

        public Color ReadyButtonColor
        {
            get => _readyButtonColor;
            set => SetProperty(ref _readyButtonColor, value);
        }

        public ICommand SwitchReadyStatusCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand GoBackCommand { get; }

        private readonly List<string> _refreshArgs;

        private readonly DispatcherTimer _timer;

        internal AwaitingRoomViewModel(NavigationService nav, bool enteredAsHost)
        {
            _nav = nav;
            _enteredAsHost = enteredAsHost;
            _app = ((App)(System.Windows.Application.Current));
            _refreshArgs = [];

            if (_enteredAsHost)
            {
                _refreshArgs.Add($"HostName:{_app.Username}");
            }
            else
            {
                _refreshArgs.Add($"GuestName:{_app.Username}");
            }

            while (_state == null)
            {
                _state = RefreshSessionState()!;
            }

            HostName = _state["HostName"];
            GuestName = _state["GuestName"];
            HostReady = bool.Parse(_state["HostReady"]!);
            GuestReady = bool.Parse(_state["GuestReady"]!);
            GuestReadyCharVisibility = false;

            StartButtonVisibility = false;

            SwitchReadyStatusCommand = new RelayCommand(OnSwitchReadyStatus);
            StartCommand = new RelayCommand(OnStart);
            GoBackCommand = new RelayCommand(OnGoBack);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {            
            _state = RefreshSessionState()!;

            if (_state == null)
            {
                OnGoBack(true);
            }
            else
            {
                HostReady = bool.Parse(_state["HostReady"]!);
                GuestReady = bool.Parse(_state["GuestReady"]!);
                GuestName = _state["GuestName"];
                GuestReadyCharVisibility = GuestName != null;

                if (HostReady && GuestReady && _enteredAsHost)
                {
                    StartButtonVisibility = true;
                }
                else
                {
                    StartButtonVisibility = false;
                }

                if (_state["GameStarted"] == "True")
                {
                    _timer.Stop();
                    long startTime = long.Parse(_state["StartTime"]!);
                    
                    while (true)
                    {
                        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() != startTime)
                        {
                            _nav.Navigate(new ArenaPage(_nav, Mode.Multiplayer, HostName, GuestName, _enteredAsHost));
                        }
                    }
                }

                _refreshArgs.Clear();
            }
        }

        private Dictionary<string, string?>? RefreshSessionState() => _app.RefreshSessionState([.. _refreshArgs]);

        private void OnSwitchReadyStatus()
        {
            string role = _enteredAsHost ? "Host" : "Guest";

            bool status = !(_enteredAsHost ? HostReady : GuestReady);

            _refreshArgs.Add($"{role}Ready:{status}");
        }

        private void OnStart()
        {
            _app.StartGame();
        }

        private void OnGoBack(object hostLeft)
        {
            _timer.Stop();

            if (_enteredAsHost)
            {
                if (_app.DeleteLobby())
                {
                    _nav.GoBack();
                }
            }
            else
            {
                if (Convert.ToBoolean(hostLeft))
                {
                    _nav.GoBack();
                }
                else if(_app.LeaveLobby())
                {
                    _nav.GoBack();
                }
            }
        }
    }
}
