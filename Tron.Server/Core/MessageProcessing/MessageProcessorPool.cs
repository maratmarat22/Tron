using Tron.Common.Messages;
using Tron.Common.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal class MessageProcessorPool
    {
        private CreateLobbyMessageProcessor? _create;
        private FetchLobbiesMessageProcessor? _fetch;
        private JoinLobbyMessageProcessor? _join;
        private ConnectionCheckMessageProcessor? _check;
        private DeleteLobbyMessageProcessor? _delete;
        private SessionStateMessageProcessor? _state;

        private readonly List<Lobby> _lobbies;

        internal MessageProcessorPool(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        internal IMessageProcessor Acquire(Header header)
        {
            return header switch
            {
                Header.CreateLobby => _create ??= new CreateLobbyMessageProcessor(_lobbies),
                Header.FetchLobbies => _fetch ??= new FetchLobbiesMessageProcessor(_lobbies),
                Header.JoinLobby => _join ??= new JoinLobbyMessageProcessor(_lobbies),
                Header.ConnectionCheck => _check ??= new ConnectionCheckMessageProcessor(),
                Header.DeleteLobby => _delete ??= new DeleteLobbyMessageProcessor(_lobbies),
                Header.SessionState => _state ??= new SessionStateMessageProcessor(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
