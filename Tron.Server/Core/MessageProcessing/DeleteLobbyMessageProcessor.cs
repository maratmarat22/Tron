using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal class DeleteLobbyMessageProcessor : ILobbyMessageProcessor
    {
        private readonly UdpUnicaster _unicaster;

        private List<Lobby> _lobbies;

        internal DeleteLobbyMessageProcessor(UdpUnicaster unicaster, List<Lobby> lobbies)
        {
            _unicaster = unicaster;
            _lobbies = lobbies;
        }

        public (Proceed, Lobby?) Process(Message message)
        {
            DeleteLobbyMessage delete = (DeleteLobbyMessage)message;

            _lobbies.RemoveAll(lobby => lobby.Hostname == delete.LobbyHostname);

            return (Proceed.False, null);
        }
    }
}
