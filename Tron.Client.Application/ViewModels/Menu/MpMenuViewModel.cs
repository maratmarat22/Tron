using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Abstract;
using Tron.Client.Application.Views.Menu;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class MpMenuViewModel : BaseViewModel
    {
        private NavigationService _nav;

        public ICommand CreateLobbyCommand { get; }
        
        public ICommand JoinLobbyCommand { get; }
        
        public ICommand InitLocalMpCommand { get; }
        
        public ICommand GoBackCommand { get; }

        internal MpMenuViewModel(NavigationService nav)
        {
            _nav = nav;

            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            JoinLobbyCommand = new RelayCommand(OnJoinLobby);
            InitLocalMpCommand = new RelayCommand(OnInitLocalMp);
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        private void OnCreateLobby()
        {
            _nav.Navigate(new CreateLobbyPage(_nav));
        }

        private void OnJoinLobby()
        {
            _nav.Navigate(new JoinLobbyPage(_nav));
        }

        private void OnInitLocalMp()
        {
            
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
