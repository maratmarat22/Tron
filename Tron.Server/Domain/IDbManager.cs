namespace Tron.Server.Domain
{
    internal interface IDbManager
    {
        internal List<Lobby> GetWaitingLobbies();

        internal void AddWaitingLobby(Lobby waitingLobby);

        internal List<string> GetTopTen();
    }
}
