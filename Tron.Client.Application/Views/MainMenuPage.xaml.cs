using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels;

namespace Tron.Client.Application.Views.Menu
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage(NavigationService nav)
        {
            DataContext = new MainMenuViewModel(nav);
            InitializeComponent();
        }
    }
}
