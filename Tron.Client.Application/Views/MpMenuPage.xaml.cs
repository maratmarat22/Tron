using System.Windows.Controls;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels;

namespace Tron.Client.Application.Views.Menu
{
    /// <summary>
    /// Interaction logic for MpMenuPage.xaml
    /// </summary>
    public partial class MpMenuPage : Page
    {
        public MpMenuPage(NavigationService nav)
        {
            DataContext = new MpMenuViewModel(nav);
            InitializeComponent();
        }
    }
}
