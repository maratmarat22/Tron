using Tron.Common.Messages;
using Tron.Common.Entities;
using Tron.Server.Networking;
using Tron.Common.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class DeleteLobbyMessageProcessor : IMessageProcessor
    {
        private readonly List<Lobby> _lobbies;

        internal DeleteLobbyMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            _lobbies.RemoveAll(lobby => lobby.Master.Equals(state!["ServerEndPoint"]));

            state!["HostName"] = "null";
            state["GuestName"] = "null";
            state["HostReady"] = "false";
            state["GuestReady"] = "false";
            state["GameStarted"] = "false";

            return new Message(Header.Ok, [request.Header.ToString()]);
        }
    }
}
