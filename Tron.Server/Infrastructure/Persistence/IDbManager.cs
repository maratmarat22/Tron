using Tron.Server.Domain.Entities;

namespace Tron.Server.Infrastructure.Persistence
{
    internal interface IDbManager
    {
        internal List<string> GetLobbies();

        internal void AddLobby(Lobby waitingLobby);

        internal List<string> GetTopTen();
    }
}
