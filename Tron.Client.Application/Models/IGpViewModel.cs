using System.Windows.Controls;
using System.Windows.Input;

namespace Tron.Client.Application.Models
{
    internal interface IGpViewModel
    {
        internal ICommand InitGameCommand { get; }

        internal Canvas Arena { get; set; }
    }
}
