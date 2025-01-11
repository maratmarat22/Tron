using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tron.Client.Application.Models;
using Tron.Client.Application.ViewModels.Game;

namespace Tron.Client.Application.Views
{
    /// <summary>
    /// Interaction logic for Arena.xaml
    /// </summary>
    public partial class ArenaPage : Page
    {
        public ArenaPage(NavigationService nav, GameMode mode, string? hostName = null, string? guestName = null, bool enteredAsHost = false)
        {
            DataContext = mode switch
            {
                GameMode.Singleplayer => new SingleplayerViewModel(nav),
                GameMode.Multiplayer => new MultiplayerViewModel(nav, hostName!, guestName!, enteredAsHost),
                GameMode.Localplayer => new LocalplayerViewModel(nav),
                _ => throw new Exception()
            };

            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CreateArenaGrid();
            Focus();
            ((GameplayViewModel)DataContext).PlayerData = PlayersGrid;
            ((GameplayViewModel)DataContext).Arena = ArenaGrid;
            ((GameplayViewModel)DataContext).InitGameCommand.Execute(null);
        }

        private void CreateArenaGrid()
        {
            int cellSize = 5;
            int rows = (int)ArenaGrid.Height / cellSize;
            int columns = (int)ArenaGrid.Width / cellSize;

            // Создание строк
            for (int i = 0; i < rows; ++i)
            {
                ArenaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(cellSize) });
            }

            // Создание столбцов
            for (int i = 0; i < columns; ++i)
            {
                ArenaGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(cellSize) });
            }

            for (int i = 0; i < rows; i += cellSize)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = i * cellSize,
                    X2 = ArenaGrid.Width,
                    Y2 = i * cellSize,
                    Stroke = Brushes.White,
                    StrokeThickness = 1
                };

                Net.Children.Add(line);
            }

            for (int i = 0; i < columns; i += cellSize)
            {
                Line line = new Line
                {
                    X1 = i * cellSize,
                    Y1 = 0,
                    X2 = i * cellSize,
                    Y2 = ArenaGrid.Height,
                    Stroke = Brushes.White,
                    StrokeThickness = 1
                };

                Net.Children.Add(line);
            }
        }
    }
}
