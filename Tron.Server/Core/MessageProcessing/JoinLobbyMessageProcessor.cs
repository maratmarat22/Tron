using Tron.Common.Messages.General;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core.MessageProcessing
{
    internal class JoinLobbyMessageProcessor : ILobbyMessageProcessor
    {
        private IDbQueryProcessor _queryProcessor;
        private UdpUnicaster _unicaster;

        internal JoinLobbyMessageProcessor(IDbQueryProcessor queryProcessor, UdpUnicaster unicaster)
        {
            _queryProcessor = queryProcessor;
            _unicaster = unicaster;
        }

        public (Proceed, Lobby) Process(Message message)
        {
            return Proceed.True;
        }
    }
}
