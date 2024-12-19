using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.ViewModels;

namespace Tron.Client.Application.Views.Gameplay
{
    /// <summary>
    /// Interaction logic for Arena.xaml
    /// </summary>
    public partial class ArenaPage : Page
    {
        public ArenaPage(NavigationService nav, int numPlayers, Mode mode)
        {
            DataContext = mode switch
            {
                Mode.Local => new LpViewModel(nav),
                _ => throw new Exception()
            };

            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
            ((IGpViewModel)DataContext).Arena = Arena;
            ((IGpViewModel)DataContext).InitGameCommand.Execute(null);
        }
    }

    public enum Mode
    {
        Offline,
        Online,
        Local
    }
}
