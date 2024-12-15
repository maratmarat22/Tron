using System.Windows;
using System.Windows.Navigation;
using Tron.Client.Application.Views.Menu;

namespace Tron.Client.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationService nav = PageContainer.NavigationService;
            ((App)(System.Windows.Application.Current)).ConnectToServer();
            PageContainer.Navigate(new MainMenuPage(nav));
        }
    }
}
