using Tron.Server.Core.Domain.Entities;
using Tron.Server.Networking;

namespace Tron.Server.Core.Domain.Services
{
    internal class GameplayService
    {
        internal Lobby Lobby { get; private set; }

        internal UdpMulticaster Multicaster { get; private set; }

        internal GameplayService(Lobby lobby, UdpMulticaster multicaster)
        {
            Lobby = lobby;
            Multicaster = multicaster;
        }

        internal Proceed Run()
        {
            return Proceed.False;
        }
    }
}
