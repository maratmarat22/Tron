using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class TopTenViewModel : BaseViewModel
    {
        // NAV & APP
        private readonly NavigationService _nav;
        private readonly App _app;

        // ELEMENTS
        internal DataGrid? TopTenGrid { get; set; }

        // COMMANDS
        public ICommand FetchTopTenCommand { get; set; }
        public ICommand GoBackCommand { get; }

        internal TopTenViewModel(NavigationService nav)
        {
            _nav = nav;
            _app = (App)System.Windows.Application.Current;

            FetchTopTenCommand = new RelayCommand(OnFetchTopTen);
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        private void OnFetchTopTen()
        {
            var topTen = _app.FetchTopTen();
            TopTenGrid!.ItemsSource = new ObservableCollection<KeyValuePair<string, int>>(topTen!);
        }

        private void OnGoBack() => _nav.Navigate(new MultiplayerMenuPage(_nav));
    }
}
