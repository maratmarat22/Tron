using System.Net;
using Tron.Common.Messages;
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
        private readonly EndPoint _point;
        private readonly UdpAcceptor _acceptor;

        internal Application(IDbQueryProcessor queryProcessor, IPAddress address, int port)
        {
            _queryProcessor = queryProcessor;
            _point = new IPEndPoint(address, port);
            _acceptor = new UdpAcceptor((IPEndPoint)_point);
        }

        internal void Run()
        {   
            while (true)
            {
                UdpUnicaster unicaster = _acceptor.Accept();

                Task.Run(() =>
                {
                    ProcessClient(unicaster);
                });
            }
        }

        private void ProcessClient(UdpUnicaster unicaster)
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
                                _queryProcessor.UpdateTopTen(lobby);
                                awaiting = new(lobby, multicaster);
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
