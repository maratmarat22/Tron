using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Menu;

namespace Tron.Client.Application.Views
{
    /// <summary>
    /// Interaction logic for JoinLobbyPage.xaml
    /// </summary>
    public partial class JoinLobbyPage : Page
    {
        public JoinLobbyPage(NavigationService nav)
        {
            DataContext = new JoinLobbyViewModel(nav);
            InitializeComponent();
        }
    }
}
