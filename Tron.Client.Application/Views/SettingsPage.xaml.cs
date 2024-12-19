using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels;

namespace Tron.Client.Application.Views.Menu
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage(NavigationService nav)
        {
            DataContext = new SettingsViewModel(nav);
            InitializeComponent();
        }
    }
}
