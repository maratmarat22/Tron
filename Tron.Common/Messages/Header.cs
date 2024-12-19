namespace Tron.Common.Messages
{
    public enum Header
    {
        // Client
        // Connection
        CONNECT = 000,
        // Pre-Lobby
        CREATE_LOBBY = 010,
        READ_LOBBIES = 011,
        JOIN_LOBBY = 012,
        // In-Lobby
        READY = 020,
        LEAVE_LOBBY = 021,
        CHANGE_COLOR = 022,
        // In-Game
        LEAVE_GAME = 030,
        DIRECTION = 031,

        // Server
        // Connection
        REDIRECT = 100,
        // Pre-Lobby
        RETURN_LOBBIES = 110,
        // In-Lobby
        START_GAME = 120,
        // In-Game
        GAME_STATE = 130,

        // Common
        END_POINT = 200,
        ACK = 201,
        BAD_REQUEST = 202,
        RESEND = 203,
        INTERNAL_SERVER_ERROR = 500,
    }
}
