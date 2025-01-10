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

        public Message Process(Message message, Dictionary<string, string?> state, ICaster caster)
        {
            EndPoint player = IPEndPoint.Parse(message.Payload[1]);
            EndPoint master = IPEndPoint.Parse(message.Payload[0]);

            caster.SendTo(new Message(Header.AddRemote, [message.Payload[1]]), master);

            Lobby lobby = _lobbies.Where(l => l.Master.Equals(message.Payload[0])).First();



            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
