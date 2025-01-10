using System.Net;
using Tron.Common.Messages;
using Tron.Common.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal class CreateLobbyMessageProcessor : IMessageProcessor
    {
        private List<Lobby> _lobbies;

        internal CreateLobbyMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            _lobbies.Add(new Lobby(state["Server"]!, message.Payload[0], bool.Parse(message.Payload[1]), message.Payload[2]));

            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
