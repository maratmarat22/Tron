namespace Tron.Common.Messages
{
    public enum Header
    {
        // Client
        // Connection
        Connect = 000,
        // Pre-Lobby
        CreateLobby = 010,
        ReadLobbies = 011,
        JoinLobby = 012,
        // In-Lobby
        Ready = 020,
        LeaveLobby = 021,
        ChangeColor = 022,
        // In-Game
        LeaveGame = 030,
        Direction = 031,

        // Server
        // Connection
        Redirect = 100,
        // Pre-Lobby
        ReturnLobbies = 110,
        // In-Lobby
        StartGame = 120,
        // In-Game
        GameState = 130,

        // Common
        OK = 200,
        Resend = 201
    }
}
