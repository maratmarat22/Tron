using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Menu;

namespace Tron.Client.Application.Views
{
    /// <summary>
    /// Interaction logic for MpMenuPage.xaml
    /// </summary>
    public partial class MultiplayerMenuPage : Page
    {
        public MultiplayerMenuPage(NavigationService nav)
        {
            DataContext = new MultiplayerMenuViewModel(nav);
            InitializeComponent();
        }
    }
}
