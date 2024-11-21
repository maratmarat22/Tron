using System.Net.Sockets;
using System.Net;

namespace Tron.Server.Domain.Entities
{
    internal class Lobby
    {
        private Socket _lobbyMaster;

        private Host _lobbyHost;

        private List<Player> _players;

        private int _maxPlayers;

        private bool _private;

        private int _password;

        internal Lobby(Socket lobbyMaster, EndPoint lobbyHost, int maxPlayers, bool @private, int password)
        {
            _lobbyMaster = lobbyMaster;
            _lobbyHost = lobbyHost;
            _players = new List<EndPoint>();
            _players.Add(lobbyHost);
            _maxPlayers = maxPlayers;
            _private = @private;
            _password = password;
        }

        public override string ToString()
        {
            return $"{_players.Count}/{_maxPlayers}/{_private}";
        }
    }
}
