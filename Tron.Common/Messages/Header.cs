namespace Tron.Common.Messages
{
    public enum Header
    {
        // Client
        // Connection
        REGISTER = 000,
        LOGIN = 001,
        CONNECT = 002,
        // Pre-Lobby
        CREATE_LOBBY = 010,
        READ_LOBBIES = 011,
        JOIN_LOBBY = 012,
        DELETE_LOBBY = 013,
        // In-Lobby
        READY = 020,
        LEAVE_LOBBY = 021,
        CHANGE_COLOR = 022,
        CHECK_USERNAME_UNIQUENESS = 023,
        // In-Game
        LEAVE_GAME = 030,
        DIRECTION = 031,

        // Server
        // Connection
        STATUS = 100,
        REDIRECT = 101,
        // Pre-Lobby
        RETURN_LOBBIES = 110,
        // In-Lobby
        RETURN_USERNAME_UNIQUENESS = 120,
        START_GAME = 121,
        // In-Game
        GAME_STATE = 130,

        // Common
        END_POINT = 200,
        ACKNOWLEDGE = 201,
        BAD_REQUEST = 202,
        RESEND = 203,
        INTERNAL_SERVER_ERROR = 500,
    }
}
