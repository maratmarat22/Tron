using Tron.Common.Messages;
using Tron.Common.Entities;
using Tron.Common.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class CreateLobbyMessageProcessor : IMessageProcessor
    {
        private List<Lobby> _lobbies;

        internal CreateLobbyMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message message, Dictionary<string, string?> state, ICaster caster)
        {
            _lobbies.Add(new Lobby(caster.Local.LocalEndPoint!.ToString()!, message.Payload[0], bool.Parse(message.Payload[1]), message.Payload[2]));

            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
