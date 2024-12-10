using Tron.Common.Messages.General;
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
                Header.CreateLobby => _createLobby ??= new CreateLobbyMessageProcessor(_queryProcessor, _unicaster),
                Header.GetLobbies => _getLobbies ??= new GetLobbiesMessageProcessor(_queryProcessor, _unicaster),
                Header.JoinLobby => _joinLobby ??= new JoinLobbyMessageProcessor(_queryProcessor, _unicaster),
                _ => throw new NotImplementedException()
            };
        }
    }
}
