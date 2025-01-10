using Tron.Common.Messages;
using Tron.Common.Entities;
using System.Text.Json;
using Tron.Common.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class FetchLobbiesMessageProcessor : IMessageProcessor
    {
        private readonly List<Lobby> _lobbies;

        internal FetchLobbiesMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            return new Message(Header.Acknowledge, [message.Header.ToString(), JsonSerializer.Serialize(_lobbies)]);
        }
    }
}
