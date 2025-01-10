using System.Net;
using Tron.Common.Messages;
using Tron.Common.Entities;
using Tron.Common.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class JoinLobbyMessageProcessor : IMessageProcessor
    {
        private readonly List<Lobby> _lobbies;

        internal JoinLobbyMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            string playerPoint = message.Payload[0];
            string masterPoint = message.Payload[1];

            return new Message(Header.AddRemote, [playerPoint, masterPoint]);
        }
    }
}
