using System.Net;
using Tron.Common.Messages.General;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;
using Tron.Server.Core.Domain.Services;
using Tron.Server.Core.MessageProcessing;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core
{
    internal class Application
    {
        private readonly IDbQueryProcessor _queryProcessor;
        private readonly IPEndPoint _point;
        private readonly UdpAcceptor _acceptor;

        internal Application(IDbQueryProcessor queryProcessor, (string address, int port) socket)
        {
            _queryProcessor = queryProcessor;
            _point = new IPEndPoint(IPAddress.Parse(socket.address), socket.port);
            _acceptor = new UdpAcceptor(_point);
        }

        internal void Run()
        {
            while (true)
            {
                UdpUnicaster unicaster = _acceptor.Accept();

                Task.Run(() =>
                {
                    MessageProcessorPool pool = new(_queryProcessor, unicaster);

                    while (true)
                    {
                        Message message = unicaster.Receive();
                        ILobbyMessageProcessor processor = pool.Acquire(message.Header);
                        (Proceed proceed, Lobby? lobby) = processor.Process(message);
                        
                        if (proceed == Proceed.True)
                        {
                            UdpMulticaster multicaster = new(unicaster);
                            ProcessGame(lobby!, multicaster);
                        }
                    }
                });
            }
        }

        private void ProcessGame(Lobby lobby, UdpMulticaster multicaster)
        {
            PlayerAwaitingService awaiting = new(lobby);

            while (true)
            {
                (Proceed proceed, GameState state) = awaiting.Run();
                
                if (proceed == Proceed.True)
                {
                    GameplayService gameplay = new(lobby);
                    (proceed, state) = gameplay.Run();

                    if (proceed == Proceed.True)
                    {
                        _queryProcessor.UpdateTopTen(state.Winner.Name, state.Winner.Points);
                        awaiting = new(state.Lobby);
                    }
                    else break;
                }
                else break;
            }
        }
    }
}
