using System.Windows.Controls;
using System.Windows.Input;

namespace Tron.Client.Application.Models
{
    internal interface IGpViewModel
    {
        internal ICommand InitGameCommand { get; }

        internal Grid PlayersGrid { get; set; }

        internal Grid ArenaGrid { get; set; }
    }
}
