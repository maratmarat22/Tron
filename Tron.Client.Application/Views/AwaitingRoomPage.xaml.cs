using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Menu;

namespace Tron.Client.Application.Views
{
    /// <summary>
    /// Interaction logic for AwaitingRoomPage.xaml
    /// </summary>
    public partial class AwaitingRoomPage : Page
    {
        public AwaitingRoomPage(NavigationService nav, bool enteredAsHost)
        {
            DataContext = new AwaitingRoomViewModel(nav, enteredAsHost);
            InitializeComponent();
        }
    }
}
