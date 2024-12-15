using Tron.Server.Core.Domain.Entities;
using Tron.Server.Networking;

namespace Tron.Server.Core.Domain.Services
{
    internal class PlayerAwaitingService
    {
        internal Lobby Lobby { get; private set; }
        
        internal UdpMulticaster Multicaster { get; private set; }

        internal PlayerAwaitingService(Lobby lobby, UdpMulticaster multicaster)
        {
            Lobby = lobby;
            Multicaster = multicaster;
        }
        
        internal Proceed Run()
        {
            while (true)
            {
                
            }

            return Proceed.True;
        }
    }
}
