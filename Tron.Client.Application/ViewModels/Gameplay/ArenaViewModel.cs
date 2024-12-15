using System.Windows.Input;
using System.Windows.Navigation;
using Tron.Client.Application.Models;
using Tron.Client.Application.Views.Gameplay;

namespace Tron.Client.Application.ViewModels.Gameplay
{
    internal class ArenaViewModel : BaseViewModel
    {
        private NavigationService _nav;
        
        private int _numPlayers;

        private Mode _mode;

        private IActionProvider _actionProvider;

        public ICommand InitGameCommand { get; }

        internal ArenaViewModel(NavigationService nav, int numPlayers, Mode mode)
        {
            _nav = nav;
            _numPlayers = numPlayers;
            _mode = mode;
            _actionProvider = mode == Mode.Singleplayer ? new SpActionProvider() : new MpActionProvider();

            InitGameCommand = new RelayCommand(OnInitGame);
        }

        private void OnInitGame()
        {
            //Player player = new Player();
        }
    }
}
