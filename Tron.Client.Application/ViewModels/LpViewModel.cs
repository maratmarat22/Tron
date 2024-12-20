using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using Tron.Client.Application.Models;
using Tron.Client.Application.Services;
using Tron.Common.Entities;
using Tron.Common.Messages;
using System.Windows;
using System.Windows.Media.Effects;
using System.Drawing;

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

        public Grid PlayersGrid { get; set; }

        public Grid ArenaGrid { get; set; }

        private bool _blackoutVisibility;
        public bool BlackoutVisibility
        {
            get => _blackoutVisibility;
            set => SetProperty(ref _blackoutVisibility, value);
        }

        internal LpViewModel(NavigationService nav)
        {
            _player1 = new Player(new PlayerCoordinates(0, 0), Colors.Red, Direction.RIGHT);
            _player2 = new Player(new PlayerCoordinates(0, 0), Colors.Blue, Direction.LEFT);

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

            BlackoutVisibility = true;
        }

        private void OnSetDirection(object? direction)
        {
            if (_service != null)
            {
                _service.SetDirection(_player1, (Direction)direction!);
            }
        }

        private void OnExtraSetDirection(object? direction)
        {
            if (_service != null)
            {
                _service.SetDirection(_player2, (Direction)direction!);
            }
        }

        private async void OnInitGame()
        {
            DisplayPlayerInfo();
            
            _player1.Coordinates = new(ArenaGrid.RowDefinitions.Count / 2, ArenaGrid.ColumnDefinitions.Count / 2 - 30);
            _player2.Coordinates = new(ArenaGrid.RowDefinitions.Count / 2, ArenaGrid.ColumnDefinitions.Count / 2 + 29);

            ArenaGrid.SetCoordinates(_player1.Shape, _player1.Coordinates);
            ArenaGrid.SetCoordinates(_player2.Shape, _player2.Coordinates);

            ArenaGrid.Children.Add(_player1.Shape);
            ArenaGrid.Children.Add(_player2.Shape);

            StartCountdown();
            await Task.Delay(TimeSpan.FromSeconds(_countdownTime + 1));

            _service = new(ArenaGrid, [_player1, _player2]);
            _service.Run();
        }

        private void StartCountdown()
        {
            CountdownMessage = (_countdownTime).ToString();
            _timer.Start();
        }

        private void DisplayPlayerInfo()
        {
            PlayersGrid.RowDefinitions.Add(new RowDefinition());
            PlayersGrid.RowDefinitions.Add(new RowDefinition());

            PlayersGrid.ColumnDefinitions.Add(new ColumnDefinition());
            PlayersGrid.ColumnDefinitions.Add(new ColumnDefinition());

            string[] players = ["PLAYER1", "PLAYER2"];
            System.Windows.Media.Color[] colors = [Colors.Red, Colors.Blue];

            FontFamily tiny = (FontFamily)((App)System.Windows.Application.Current).Resources["Tiny"];
            SolidColorBrush white = new SolidColorBrush(Colors.White);

            for (int i = 0; i < players.Length; ++i)
            {
                TextBlock nameTextBlock = new TextBlock
                {
                    Text = players[i],
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = white,
                    FontFamily = tiny,
                    FontSize = 40,
                    Effect = new DropShadowEffect
                    {
                        Color = colors[i],
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    }
                };

                TextBlock winsTextBlock = new TextBlock
                {
                    Text = "0",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = white,
                    FontFamily = tiny,
                    FontSize = 40,
                    Effect = new DropShadowEffect
                    {
                        Color = colors[i],
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    }
                };

                PlayersGrid.Children.Add(nameTextBlock);
                PlayersGrid.Children.Add(winsTextBlock);

                Grid.SetRow(nameTextBlock, i);
                Grid.SetColumn(nameTextBlock, 0);

                Grid.SetRow(winsTextBlock, i);
                Grid.SetColumn(winsTextBlock, 1);
            }
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
                BlackoutVisibility = false;
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
