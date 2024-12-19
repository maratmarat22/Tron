using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core.MessageProcessing
{
    internal class MessageProcessorPool
    {
        private readonly IDbQueryProcessor _queryProcessor;
        private readonly UdpUnicaster _unicaster;
        private CreateLobbyMessageProcessor? _createLobby;
        private GetLobbiesMessageProcessor? _getLobbies;
        private JoinLobbyMessageProcessor? _joinLobby;

        internal MessageProcessorPool(IDbQueryProcessor queryProcessor, UdpUnicaster unicaster)
        {
            _queryProcessor = queryProcessor;
            _unicaster = unicaster;
        }

        internal ILobbyMessageProcessor Acquire(Header header)
        {
            return header switch
            {
                Header.CREATE_LOBBY => _createLobby ??= new CreateLobbyMessageProcessor(_queryProcessor, _unicaster),
                Header.READ_LOBBIES => _getLobbies ??= new GetLobbiesMessageProcessor(_queryProcessor, _unicaster),
                Header.JOIN_LOBBY => _joinLobby ??= new JoinLobbyMessageProcessor(_queryProcessor, _unicaster),
                _ => throw new NotImplementedException()
            };
        }
    }
}
