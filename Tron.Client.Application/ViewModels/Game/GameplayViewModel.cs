using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using Tron.Client.Application.Models;
using Tron.Client.Application.Services;

namespace Tron.Client.Application.ViewModels.Game
{
    internal abstract class GameplayViewModel : BaseViewModel
    {
        protected readonly NavigationService _nav;

        protected readonly List<Player> _players;
        protected GameplayService? _service;

        protected readonly DispatcherTimer _countdownTimer;
        protected int _countdownTime;
        protected string _countdownMessage;

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

        public Grid? PlayersGrid { get; set; }

        public Grid? ArenaGrid { get; set; }

        public ICommand InitGameCommand { get; }

        public ICommand SetDirectionCommand { get; }

        public ICommand ExtraSetDirectionCommand { get; }

        public ICommand GoBackCommand { get; }

        internal GameplayViewModel(NavigationService nav)
        {
            _nav = nav;

            _players = [];

            _countdownTimer = new DispatcherTimer();
            _countdownTimer.Interval = TimeSpan.FromSeconds(1);
            _countdownTimer.Tick += CountdownTimer_Tick;

            _countdownTime = 3;
            _countdownMessage = _countdownTime.ToString();
            _countdownVisibility = false;
            _blackoutVisibility = false;

            InitGameCommand = new RelayCommand(OnInitGame);
            SetDirectionCommand = new RelayCommand(OnSetDirection);
            ExtraSetDirectionCommand = new RelayCommand(OnExtraSetDirection);
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        protected abstract void OnInitGame();

        protected abstract void OnSetDirection(object? direction);

        protected abstract void OnExtraSetDirection(object? direction);

        protected abstract void OnGoBack();

        protected abstract void AllocatePlayers();

        protected void DisplayPlayerInfo()
        {
            PlayersGrid!.RowDefinitions.Add(new RowDefinition());
            PlayersGrid.RowDefinitions.Add(new RowDefinition());

            FontFamily tiny = (FontFamily)((App)System.Windows.Application.Current).Resources["Tiny"];
            SolidColorBrush white = new SolidColorBrush(Colors.White);

            for (int i = 0; i < _players.Count; ++i)
            {
                TextBlock nameTextBlock = new TextBlock
                {
                    Text = _players[i].Name,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = white,
                    FontFamily = tiny,
                    FontSize = 40,
                    Effect = new DropShadowEffect
                    {
                        Color = _players[i].PlayerColor,
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
                    FontSize = 20,
                    Effect = new DropShadowEffect
                    {
                        Color = _players[i].PlayerColor,
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    }
                };

                TextBlock scoreTextBlock = new TextBlock
                {
                    Text = "0pts",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = white,
                    FontFamily = tiny,
                    FontSize = 20,
                    Effect = new DropShadowEffect
                    {
                        Color = _players[i].PlayerColor,
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    }
                };

                PlayersGrid.Children.Add(nameTextBlock);
                PlayersGrid.Children.Add(winsTextBlock);
                PlayersGrid.Children.Add(scoreTextBlock);

                Grid.SetRow(nameTextBlock, i);
                Grid.SetColumn(nameTextBlock, 0);

                Grid.SetRow(winsTextBlock, i);
                Grid.SetColumn(winsTextBlock, 1);

                Grid.SetRow(scoreTextBlock, i);
                Grid.SetColumn(scoreTextBlock, 2);
            }
        }

        protected async Task CountDown()
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

        protected void UpdatePlayerInfo(Player player)
        {
            foreach (object child in PlayersGrid!.Children)
            {
                if (child is TextBlock textBlock)
                {
                    int row = Grid.GetRow(textBlock);
                    int column = Grid.GetColumn(textBlock);

                    if (column == 0 && textBlock.Text == player.Name)
                    {
                        foreach (object sibling in PlayersGrid.Children)
                        {
                            if (sibling is TextBlock siblingTextBlock && Grid.GetRow(siblingTextBlock) == row)
                            {
                                int siblingColumn = Grid.GetColumn(siblingTextBlock);
                                if (siblingColumn == 1)
                                {
                                    siblingTextBlock.Text = player.Wins.ToString();
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
}
