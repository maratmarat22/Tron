using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal class JoinLobbyMessageProcessor : ILobbyMessageProcessor
    {
        private UdpUnicaster _unicaster;

        private List<Lobby> _lobbies;

        internal JoinLobbyMessageProcessor(UdpUnicaster unicaster, List<Lobby> lobbies)
        {
            _unicaster = unicaster;
            _lobbies = lobbies;
        }

        public (Proceed, Lobby) Process(Message message)
        {
            JoinLobbyMessage join = (JoinLobbyMessage)message;

            Lobby lobby = _lobbies.Where(lobby => lobby.Hostname == join.LobbyHostname).First();
            ReturnLobbiesMessage @return = new([lobby.ToString()]);

            _unicaster.Send(@return);

            return (Proceed.True, lobby);
        }
    }
}
