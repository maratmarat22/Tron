using Tron.Common.Messages;
using Tron.Common.Entities;
using Tron.Common.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class DeleteLobbyMessageProcessor : IMessageProcessor
    {
        private List<Lobby> _lobbies;

        internal DeleteLobbyMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            _lobbies.RemoveAll(lobby => lobby.Master.Equals(state["Server"]));

            state["HostName"] = null;
            state["GuestName"] = null;
            state["HostReady"] = "False";
            state["GuestReady"] = "False";
            state["GameStarted"] = "False";

            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
