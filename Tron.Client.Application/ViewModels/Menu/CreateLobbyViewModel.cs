using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.ViewModels.Abstract;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class CreateLobbyViewModel : BaseViewModel
    {
        private NavigationService _nav;

        private int _maxPlayers;
        public int MaxPlayers
        {
            get => _maxPlayers;
            set
            {
                if (value >= 2 && value <= 4)
                    SetProperty(ref _maxPlayers, value);
            }
        }

        private string _privacyMode;
        public string PrivacyMode
        {
            get => _privacyMode;
            set => SetProperty(ref _privacyMode, value);
        }
        
        public ICommand IncreaseMaxPlayersCommand { get; }

        public ICommand DecreaseMaxPlayersCommand { get; }

        public ICommand ChangePrivacyModeCommand {  get; }

        public ICommand CreateLobbyCommand { get; }

        public ICommand GoBackCommand { get; }

        internal CreateLobbyViewModel(NavigationService nav)
        {
            _nav = nav;
            
            _maxPlayers = 2;
            _privacyMode = "PUBLIC";

            IncreaseMaxPlayersCommand = new RelayCommand(OnIncreaseMaxPlayers);
            DecreaseMaxPlayersCommand = new RelayCommand(OnDecreaseMaxPlayers);
            ChangePrivacyModeCommand = new RelayCommand(OnChangePrivacyMode);
            CreateLobbyCommand = new RelayCommand(OnCreateLobby);
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        private void OnIncreaseMaxPlayers()
        {
                ++MaxPlayers;
        }

        private void OnDecreaseMaxPlayers()
        {
                --MaxPlayers;
        }

        private void OnChangePrivacyMode()
        {
            PrivacyMode = PrivacyMode == "PUBLIC" ? "PRIVATE" : "PUBLIC";
        }

        private void OnCreateLobby()
        {

        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
