using Tron.Common.Messages;
using Tron.Common.Entities;
using Tron.Server.Networking;
using Tron.Common.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class CreateLobbyMessageProcessor : IMessageProcessor
    {
        private readonly List<Lobby> _lobbies;

        internal CreateLobbyMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            var masterEndPoint = state!["ServerEndPoint"];
            var hostName = request.Payload[0];
            var isPrivate = bool.Parse(request.Payload[1]);
            var password = request.Payload[2];

            Lobby lobby = new(masterEndPoint, hostName, isPrivate, password);
            _lobbies.Add(lobby);

            return new Message(Header.Ok, [request.Header.ToString()]);
        }
    }
}
