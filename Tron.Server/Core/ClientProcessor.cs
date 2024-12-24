using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;
using Tron.Server.Core.Domain.Services;
using Tron.Server.Core.MessageProcessing;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core
{
    internal class ClientProcessor
    {
        private readonly UdpUnicaster _unicaster;
        private readonly IDbQueryProcessor _queryProcessor;

        private List<Lobby> _lobbies;

        internal ClientProcessor(UdpUnicaster unicaster, IDbQueryProcessor queryProcessor, List<Lobby> lobbies)
        {
            _unicaster = unicaster;
            _queryProcessor = queryProcessor;

            _lobbies = lobbies;
        }

        internal void Process()
        {
            MessageProcessorPool pool = new(_unicaster, _lobbies);

            while (true)
            {
                Message message = _unicaster.Receive(Header.CREATE_LOBBY, Header.READ_LOBBIES, Header.JOIN_LOBBY);
                ILobbyMessageProcessor processor = pool.Acquire(message.Header);
                (Proceed proceed, Lobby? lobby) = processor.Process(message);

                if (proceed == Proceed.True)
                {
                    UdpMulticaster multicaster = new(_unicaster);

                    PlayerAwaitingService awaiting = new(lobby!, multicaster);

                    while (true)
                    {
                        proceed = awaiting.Run();

                        if (proceed == Proceed.True)
                        {
                            GameplayService gameplay = new(lobby!, multicaster);
                            proceed = gameplay.Run();

                            if (proceed == Proceed.True)
                            {
                                _queryProcessor.UpdatePlayer();
                                awaiting = new(lobby!, multicaster);
                            }
                            else break;
                        }
                        else break;
                    }
                }
            }
        }
    }
}
