using Tron.Common.Messages.General;
using Tron.Common.Messages.Lobby;
using Tron.Common.Networking;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;
using Tron.Server.Core.Domain.Entities;

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
            Lobby lobby = new(msg.MaxPlayers, msg.Private, msg.Password);
            _queryProcessor.CreateLobby(lobby);
            UdpMulticaster multicaster = new UdpMulticaster(_unicaster);

            return (Proceed.True, lobby);
        }
    }
}
