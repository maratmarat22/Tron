using Tron.Common.Messages;
using Tron.Common.Entities;
using Tron.Server.Persistence.QueryProcessing;
using Tron.Server.Core.Messages.Processors;

namespace Tron.Server.Core.Messages
{
    internal class MessageProcessorPool
    {
        private CreateLobbyMessageProcessor? _CL;
        private FetchLobbiesMessageProcessor? _FL;
        private JoinLobbyMessageProcessor? _JL;
        private ConnectionCheckMessageProcessor? _CC;
        private DeleteLobbyMessageProcessor? _DL;
        private SessionStateMessageProcessor? _SS;
        private AddRemoteMessageProcessor? _AR;
        private LeaveLobbyMessageProcessor? _LL;
        private FetchDirectionsMessageProcessor? _FD;
        private FetchTopTenMessageProcessor? _FTT;
        private StartGameMessageProcessor? _SG;
        private AddScoreMessageProcessor? _AS;

        private readonly List<Lobby> _lobbies;
        private readonly IDbQueryProcessor _queryProcessor;

        internal MessageProcessorPool(List<Lobby> lobbies, IDbQueryProcessor queryProcessor)
        {
            _lobbies = lobbies;
            _queryProcessor = queryProcessor;
        }

        internal (IMessageProcessor processor, bool stateRequired, bool unicasterRequired, bool multicasterRequired) Acquire(Header header)
        {
            return header switch
            {
                Header.CreateLobby => (_CL ??= new CreateLobbyMessageProcessor(_lobbies), true, false, false),
                Header.FetchLobbies => (_FL ??= new FetchLobbiesMessageProcessor(_lobbies), false, true, false),
                Header.JoinLobby => (_JL ??= new JoinLobbyMessageProcessor(), false, true, false),
                Header.ConnectionCheck => (_CC ??= new ConnectionCheckMessageProcessor(), false, false, false),
                Header.DeleteLobby => (_DL ??= new DeleteLobbyMessageProcessor(_lobbies), true, false, true),
                Header.SessionState => (_SS ??= new SessionStateMessageProcessor(), true, false, false),
                Header.AddRemote => (_AR ??= new AddRemoteMessageProcessor(), false, false, true),
                Header.LeaveLobby => (_LL ??= new LeaveLobbyMessageProcessor(), true, false, true),
                Header.FetchDirections => (_FD ??= new FetchDirectionsMessageProcessor(), true, false, false),
                Header.FetchTopTen => (_FTT ??= new FetchTopTenMessageProcessor(_queryProcessor), false, false, false),
                Header.StartGame => (_SG ??= new StartGameMessageProcessor(), true, false, false),
                Header.AddScore => (_AS ??= new AddScoreMessageProcessor(_queryProcessor), false, false, false),
                _ => throw new NotImplementedException()
            };
        }
    }
}
