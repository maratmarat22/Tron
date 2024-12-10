using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.Domain.Services
{
    internal class GameplayService
    {
        internal Lobby Lobby { get; private set; }

        internal GameplayService(Lobby lobby)
        {
            Lobby = lobby;
        }

        internal (Proceed, GameState) Run()
        {

        }
    }
}
