using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Abstract;
using Tron.Client.Application.Views.Menu;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class MainMenuViewModel : BaseViewModel
    {
        private NavigationService _nav;

        public ICommand InitSpCommand { get; }
        
        public ICommand NavToMpMenuCommand { get; }
        
        public ICommand NavToSettingsCommand { get; }
        
        public ICommand ExitCommand { get; }

        internal MainMenuViewModel(NavigationService nav)
        {
            _nav = nav;

            InitSpCommand = new RelayCommand(OnInitSp);
            NavToMpMenuCommand = new RelayCommand(OnNavToMpMenu);
            NavToSettingsCommand = new RelayCommand(OnNavToSettings);
            ExitCommand = new RelayCommand(OnExit);
        }

        private void OnInitSp()
        {
            
        }

        private void OnNavToMpMenu()
        {
            _nav.Navigate(new MpMenuPage(_nav));
        }

        private void OnNavToSettings()
        {
            _nav.Navigate(new SettingsPage(_nav));
        }

        private void OnExit()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
