using System.Windows.Media.Effects;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using Tron.Client.Application.Services;
using Tron.Client.Application.Models;

namespace Tron.Client.Application.ViewModels.Game
{
    internal abstract class GameplayViewModel : BaseViewModel
    {
        protected readonly NavigationService _nav;

        protected readonly List<Player> _players;

        protected GameplayService? _service;

        protected readonly DispatcherTimer _countdownTimer;
        protected int _countdownTime;

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

        private bool _blackoutVisibility;
        public bool BlackoutVisibility
        {
            get => _blackoutVisibility;
            set => SetProperty(ref _blackoutVisibility, value);
        }

        public Grid? PlayerData { get; set; }

        public Grid? Arena { get; set; }

        public ICommand InitGameCommand { get; }

        public ICommand SetDirectionCommand { get; }

        public ICommand ExtraSetDirectionCommand { get; }

        public ICommand GoBackCommand { get; }

        private string _winner;
        
        public string Winner
        {
            get => _winner;
            set => SetProperty(ref _winner, value);
        }

        private Color _winnerColor;

        public Color WinnerColor
        {
            get => _winnerColor;
            set => SetProperty(ref _winnerColor, value);
        }

        internal GameplayViewModel(NavigationService nav)
        {
            _nav = nav;

            _players = [];

            _countdownTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _countdownTimer.Tick += CountdownTimer_Tick;

            _countdownTime = 3;
            _countdownMessage = _countdownTime.ToString();
            CountdownVisibility = false;
            BlackoutVisibility = false;

            InitGameCommand = new RelayCommand(OnInitGame);
            SetDirectionCommand = new RelayCommand(OnSetDirection);
            ExtraSetDirectionCommand = new RelayCommand(OnExtraSetDirection);
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        protected void AllocatePlayers()
        {
            _players![0].InitialCoordinates = new(Arena!.RowDefinitions.Count / 2, Arena.ColumnDefinitions.Count / 2 - 30);
            _players[1].InitialCoordinates = new(Arena.RowDefinitions.Count / 2, Arena.ColumnDefinitions.Count / 2 + 29);

            foreach (Player player in _players)
            {
                Arena.SetCoordinates(player.Shape, player.Coordinates);
                Arena.Children.Add(player.Shape);
            }
        }

        protected void CreatePlayerData()
        {
            PlayerData!.RowDefinitions.Add(new RowDefinition());
            PlayerData.RowDefinitions.Add(new RowDefinition());

            FontFamily tiny = (FontFamily)((App)System.Windows.Application.Current).Resources["Tiny"];
            SolidColorBrush white = new(Colors.White);

            for (int i = 0; i < _players.Count; ++i)
            {
                TextBlock name = new()
                {
                    Text = _players[i].Name,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = white,
                    FontFamily = tiny,
                    FontSize = 30,
                    Effect = new DropShadowEffect
                    {
                        Color = _players[i].PlayerColor,
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    }
                };

                TextBlock lives = new()
                {
                    Text = ((int)(Constants.Lives)).ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = white,
                    FontFamily = tiny,
                    FontSize = 30,
                    Effect = new DropShadowEffect
                    {
                        Color = _players[i].PlayerColor,
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    }
                };

                TextBlock score = new()
                {
                    Text = "0pts",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = white,
                    FontFamily = tiny,
                    FontSize = 30,
                    Effect = new DropShadowEffect
                    {
                        Color = _players[i].PlayerColor,
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    }
                };

                PlayerData.Children.Add(name);
                PlayerData.Children.Add(lives);
                PlayerData.Children.Add(score);

                Grid.SetRow(name, i);
                Grid.SetColumn(name, 0);

                Grid.SetRow(lives, i);
                Grid.SetColumn(lives, 1);

                Grid.SetRow(score, i);
                Grid.SetColumn(score, 2);
            }
        }

        internal void UpdatePlayerData()
        {
            foreach (var player in _players)
            {
                foreach (object child in PlayerData!.Children)
                {
                    if (child is TextBlock textBlock)
                    {
                        int row = Grid.GetRow(textBlock);
                        int column = Grid.GetColumn(textBlock);

                        if (column == 0 && textBlock.Text == player.Name)
                        {
                            foreach (object sibling in PlayerData.Children)
                            {
                                if (sibling is TextBlock siblingTextBlock && Grid.GetRow(siblingTextBlock) == row)
                                {
                                    int siblingColumn = Grid.GetColumn(siblingTextBlock);
                                    if (siblingColumn == 1)
                                    {
                                        siblingTextBlock.Text = player.Lives.ToString();
                                    }
                                    else if (siblingColumn == 2)
                                    {
                                        siblingTextBlock.Text = player.Score + "pts";
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        internal async Task CountDown()
        {
            BlackoutVisibility = true;
            CountdownVisibility = true;

            _countdownTime = 3;
            CountdownMessage = _countdownTime.ToString();
            _countdownTimer.Start();

            await Task.Delay(TimeSpan.FromSeconds(_countdownTime + 1));
        }

        private async void CountdownTimer_Tick(object? sender, EventArgs e)
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
                _countdownTimer.Stop();
                CountdownVisibility = false;
            }
        }

        protected void DisplayWinner(string? winner, Color color)
        {
            BlackoutVisibility = true;

            if (winner != null)
            {
                Winner = "WINNER\n" + winner;
                WinnerColor = color;
            }
            else
            {
                Winner = "DRAW";
                WinnerColor = Colors.Violet;
            }
        }

        protected abstract void OnInitGame();

        protected abstract void OnSetDirection(object? direction);

        protected abstract void OnExtraSetDirection(object? direction);

        protected abstract void OnGoBack();
    }
}
