using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Menu;

namespace Tron.Client.Application.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage(NavigationService nav)
        {
            DataContext = new RegisterViewModel(nav);
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((RegisterViewModel)DataContext).RegisterTextBox = RegisterTextBox;
        }
    }
}
