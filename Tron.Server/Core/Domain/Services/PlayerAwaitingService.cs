using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.Domain.Services
{
    internal class PlayerAwaitingService
    {
        internal Lobby Lobby { get; private set; }
        
        internal PlayerAwaitingService(Lobby lobby)
        {
            Lobby = lobby;
        }
        
        internal (Proceed, GameState) Run()
        {
            while (true)
            {
                break;
            }

            return (Proceed.True, new GameState());
        }
    }
}
