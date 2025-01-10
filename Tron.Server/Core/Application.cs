using System.Net;
using Tron.Server.Core.MessageProcessing;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Common.Entities;

namespace Tron.Server.Core
{
    internal class Application
    {
        private readonly IDbQueryProcessor _queryProcessor;
        private readonly EndPoint _point;
        private readonly Acceptor _acceptor;

        private readonly List<Lobby> _lobbies;
        private readonly MessageProcessorPool _pool;

        internal Application(IDbQueryProcessor queryProcessor, IPAddress address, int port)
        {
            _queryProcessor = queryProcessor;
            _point = new IPEndPoint(address, port);
            _acceptor = new Acceptor((IPEndPoint)_point, _queryProcessor);

            _lobbies = [];
            _pool = new(_lobbies);
        }

        internal void Run()
        {   
            while (true)
            {
                Unicaster? unicaster = _acceptor.Accept();

                if (unicaster != null)
                {
                    Task.Run(() =>
                    {
                        ICaster caster = unicaster;

                        Dictionary<string, string?> state = new()
                        {
                            { "ServerPoint", unicaster.Local.LocalEndPoint!.ToString()! },
                            { "HostName", null },
                            { "GuestName", null },
                            { "HostReady", "False" },
                            { "GuestReady", "False" },
                            { "GameStarted", "False" }
                        };

                        while (true)
                        {
                            Message? message = caster.Receive();

                            if (message != null)
                            {                                
                                IMessageProcessor processor = _pool.Acquire(message.Header);

                                Message? response = processor.Process(message, state, caster);

                                if (response != null)
                                {
                                    caster.Send(response);

                                    if (message.Header == Header.CreateLobby)
                                    {
                                        caster = new Multicaster(unicaster.Local, unicaster.Remote);
                                    }
                                }
                                else
                                {
                                    unicaster.Send(new Message(Header.Resend, []));
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    });
                }
            }
        }
    }
}
