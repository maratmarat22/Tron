using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Menu;

namespace Tron.Client.Application.Views
{
    /// <summary>
    /// Interaction logic for CreateLobbyPage.xaml
    /// </summary>
    public partial class CreateLobbyPage : Page
    {
        public CreateLobbyPage(NavigationService nav)
        {
            DataContext = new CreateLobbyViewModel(nav);
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as CreateLobbyViewModel)!.PasswordTextBox = PasswordTextBox;
        }
    }
}
