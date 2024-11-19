namespace Tron.Server.Domain
{
    internal interface IDbManager
    {
        internal List<WaitingLobby> GetWaitingLobbies();

        internal void AddWaitingLobby(WaitingLobby waitingLobby);

        internal List<string> GetTopTen();
    }
}
