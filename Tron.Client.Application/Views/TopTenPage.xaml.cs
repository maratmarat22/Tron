using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tron.Client.Application.ViewModels.Menu;

namespace Tron.Client.Application.Views
{
    /// <summary>
    /// Interaction logic for TopTenPage.xaml
    /// </summary>
    public partial class TopTenPage : Page
    {
        public TopTenPage(NavigationService nav)
        {
            DataContext = new TopTenViewModel(nav);
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var context = (DataContext as TopTenViewModel)!;
            context.TopTenGrid = TopTenGrid;
            context.FetchTopTenCommand.Execute(null);
        }
    }
}
