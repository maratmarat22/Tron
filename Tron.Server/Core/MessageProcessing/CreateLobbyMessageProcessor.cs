using Tron.Common.Networking;
using Tron.Server.Persistence.QueryProcessing;
using Tron.Server.Core.Domain.Entities;
using Tron.Common.Messages;
using Tron.Server.Core;

namespace Tron.Server.Core.MessageProcessing
{
    internal class CreateLobbyMessageProcessor : ILobbyMessageProcessor
    {
        private readonly IDbQueryProcessor _queryProcessor;
        private readonly UdpUnicaster _unicaster;

        internal CreateLobbyMessageProcessor(IDbQueryProcessor queryProcessor, UdpUnicaster unicaster)
        {
            _queryProcessor = queryProcessor;
            _unicaster = unicaster;
        }

        public (Proceed, Lobby) Process(Message message)
        {
            CreateLobbyMessage msg = (CreateLobbyMessage)message;
            Lobby lobby = new(0, msg.MaxPlayers, msg.IsPrivate, msg.Password);
            _queryProcessor.CreateLobby(lobby);

            return (Proceed.True, lobby);
        }
    }
}
