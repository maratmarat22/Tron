namespace Tron.Common.Messages
{
    public enum Header
    {
        // Client
        // Connection
        Register = 000,
        LogIn = 001,
        Connect = 002,
        ConnectionCheck = 003,
        // Pre-Lobby
        CreateLobby = 010,
        FetchLobbies = 011,
        JoinLobby = 012,
        DeleteLobby = 013,
        // In-Lobby
        Ready = 020,
        NotReady = 021,
        LeaveLobby = 022,
        CHANGE_COLOR = 023,
        CHECK_USERNAME_UNIQUENESS = 024,
        // In-Game
        LEAVE_GAME = 030,
        DIRECTION = 031,

        // Server
        // Connection
        STATUS = 100,
        REDIRECT = 101,
        MULTICASTER = 102,
        // Pre-Lobby
        RETURN_LOBBIES = 110,
        // In-Lobby
        StartGame = 121,
        // In-Game
        GAME_STATE = 130,

        // Common
        EndPoint = 200,
        Acknowledge = 201,
        BAD_REQUEST = 202,
        Resend = 203,
        INTERNAL_SERVER_ERROR = 500,
        SessionState,
        AddRemote,
        FetchDirections
    }
}
