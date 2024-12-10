using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Persistence.QueryProcessing
{
    internal interface IDbQueryProcessor
    {
        internal int CreateLobby(Lobby lobby);

        internal string[] ReadLobbies();

        internal void UpdateLobby(Lobby lobby);

        internal void DeleteLobby(Lobby lobby);

        internal string[] ReadTopTen();

        internal void UpdateTopTen(string player, int points);
    }
}
