using System.Windows.Navigation;
using System.Windows;
using Tron.Client.Application.Views;

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
            PageContainer.Navigate(new MainMenuPage(nav));
        }
    }
}
