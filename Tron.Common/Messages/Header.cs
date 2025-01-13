namespace Tron.Common.Messages
{
    public enum Header
    {
        // Auth
        Register,
        LogIn,

        // Lobbies
        CreateLobby,
        FetchLobbies,
        JoinLobby,
        DeleteLobby,
        LeaveLobby,

        // TopTen
        FetchTopTen,

        // Game
        LeaveGame,
        StartGame,
        SessionState,
        FetchDirections,

        // Status
        Ok,
        Nok,

        // Connection
        Connect,
        ConnectionCheck,
        InternalServerError,
        AddRemote
    }
}
