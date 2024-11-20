using System.Net.Sockets;
using System.Net;

namespace Tron.Server.Domain
{
    internal class WaitingLobby
    {
        private Socket _lobbyMaster;

        private EndPoint _lobbyHost;

        private List<EndPoint> _players;

        private int _maxPlayers;

        private bool _private;

        private int _code;

        internal WaitingLobby(Socket lobbyMaster, EndPoint lobbyHost, int maxPlayers, bool @private, int code)
        {
            _lobbyMaster = lobbyMaster;
            _lobbyHost = lobbyHost;
            _players = new List<EndPoint>();
            _players.Add(lobbyHost);
            _maxPlayers = maxPlayers;
            _private = @private;
            _code = code;
        }

        public override string ToString()
        {
            return $"{_players.Count}/{_maxPlayers}/{_private}";
        }
    }
}
