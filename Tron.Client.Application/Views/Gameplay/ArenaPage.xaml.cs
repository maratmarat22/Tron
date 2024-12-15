using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Gameplay;

namespace Tron.Client.Application.Views.Gameplay
{
    /// <summary>
    /// Interaction logic for Arena.xaml
    /// </summary>
    public partial class ArenaPage : Page
    {
        public ArenaPage(NavigationService nav, int numPlayers, Mode mode)
        {
            DataContext = new ArenaViewModel(nav, numPlayers, mode);
            InitializeComponent();
        }
    }

    public enum Mode
    {
        Singleplayer,
        Multiplayer
    }
}