using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal class MessageProcessorPool
    {
        private readonly UdpUnicaster _unicaster;
        private CreateLobbyMessageProcessor? _createLobby;
        private ReadLobbiesMessageProcessor? _getLobbies;
        private JoinLobbyMessageProcessor? _joinLobby;

        private List<Lobby> _lobbies;

        internal MessageProcessorPool(UdpUnicaster unicaster, List<Lobby> lobbies)
        {
            _unicaster = unicaster;

            _lobbies = lobbies;
        }

        internal ILobbyMessageProcessor Acquire(Header header)
        {
            return header switch
            {
                Header.CREATE_LOBBY => _createLobby ??= new CreateLobbyMessageProcessor(_unicaster, _lobbies),
                Header.READ_LOBBIES => _getLobbies ??= new ReadLobbiesMessageProcessor(_unicaster, _lobbies),
                Header.JOIN_LOBBY => _joinLobby ??= new JoinLobbyMessageProcessor(_unicaster, _lobbies),
                _ => throw new NotImplementedException()
            };
        }
    }
}
