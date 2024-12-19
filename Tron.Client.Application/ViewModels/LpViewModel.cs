using System.Drawing;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using Tron.Client.Application.Models;
using Tron.Client.Application.Services;
using Tron.Common.Messages;

namespace Tron.Client.Application.ViewModels
{
    internal class LpViewModel : BaseViewModel, IGpViewModel
    {
        private NavigationService _nav;

        private Player _player1;
        private Player _player2;
        private LocalGameplayService _service;

        private DispatcherTimer _timer;
        private int _countdownTime;
        
        private string _countdownMessage;
        public string CountdownMessage
        {
            get => _countdownMessage;
            set => SetProperty(ref _countdownMessage, value);
        }

        private bool _countdownVisibility;
        public bool CountdownVisibility
        {
            get => _countdownVisibility;
            set => SetProperty(ref _countdownVisibility, value);
        }

        public ICommand InitGameCommand { get; }

        public ICommand SetDirectionCommand { get; }

        public ICommand ExtraSetDirectionCommand { get; }

        public ICommand GoBackCommand { get; }

        public Canvas Arena { get; set; }

        internal LpViewModel(NavigationService nav)
        {
            _player1 = new Player(new Point(0, 0), Colors.Red, Direction.RIGHT);
            _player2 = new Player(new Point(0, 0), Colors.Blue, Direction.LEFT);

            _nav = nav;

            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            _countdownTime = 3;
            _countdownMessage = _countdownTime.ToString();
            _countdownVisibility = true;

            InitGameCommand = new RelayCommand(OnInitGame);
            GoBackCommand = new RelayCommand(OnGoBack);
            SetDirectionCommand = new RelayCommand(OnSetDirection);
            ExtraSetDirectionCommand = new RelayCommand(OnExtraSetDirection);
        }

        private void OnSetDirection(object? direction)
        {
            if (_service != null)
            {
                _player1.Direction = (Direction)direction!;
            }
        }

        private void OnExtraSetDirection(object? direction)
        {
            if (_service != null)
            {
                _player2.Direction = (Direction)direction!;
            }
        }

        private async void OnInitGame()
        {
            _player1.Position = new((int)(Arena.Width / 2 - 200), (int)(Arena.Height / 2));
            _player2.Position = new((int)(Arena.Width / 2 + 180), (int)(Arena.Height / 2));

            Arena.Children.Add(_player1.Shape);
            Arena.Children.Add(_player2.Shape);

            Canvas.SetLeft(_player1.Shape, _player1.Position.X);
            Canvas.SetTop(_player1.Shape, _player1.Position.Y);

            Canvas.SetLeft(_player2.Shape, _player2.Position.X);
            Canvas.SetTop(_player2.Shape, _player2.Position.Y);

            StartCountDown();
            await Task.Delay(TimeSpan.FromSeconds(_countdownTime));

            _service = new(Arena, [_player1, _player2]);
            _service.Run();
        }

        private void StartCountDown()
        {
            CountdownMessage = (_countdownTime).ToString();
            _timer.Start();
        }

        private async void Timer_Tick(object? sender, EventArgs e)
        {
            if (_countdownTime > 1)
            {
                CountdownMessage = (--_countdownTime).ToString();
            }
            else if (_countdownTime == 1)
            {
                CountdownMessage = "GO";
                await Task.Delay(TimeSpan.FromSeconds(1));
                _timer.Stop();
                CountdownVisibility = false;
            }
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
