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
                        Dictionary<string, string?> state = new()
                        {
                            { "Server", unicaster.Local.LocalEndPoint!.ToString()! },
                            { "HostName", null },
                            { "GuestName", null },
                            { "HostReady", "False" },
                            { "GuestReady", "False" },
                            { "GameStarted", "False" }
                        };

                        while (true)
                        {
                            Message? message = unicaster.Receive();

                            if (message != null)
                            {
                                if(message.Header == Header.Acknowledge)
                                    { }
                                IMessageProcessor processor = _pool.Acquire(message.Header);

                                Message? response = processor.Process(message, state, unicaster);

                                if (response != null)
                                {
                                    if (message.Header == Header.JoinLobby)
                                    {
                                        if (unicaster.Local.TrySendTo(new Message(Header.AddRemote, [response.Payload[0]]), IPEndPoint.Parse(response.Payload[1])))
                                        {
                                            if (unicaster.Local.TryReceiveFrom(IPEndPoint.Parse(response.Payload[1])) != null)
                                            {
                                                unicaster.Send(new Message(Header.Acknowledge, [message.Header.ToString()]));
                                            }
                                        }
                                    }
                                    else if (message.Header == Header.CreateLobby)
                                    {
                                        unicaster.Send(response);
                                        Multicaster multicaster = new(unicaster.Local, unicaster.Remote);
                                        MulticastService service = new(multicaster, state, _pool);
                                        service.Run();
                                    }
                                    else
                                    {
                                        unicaster.Send(response);
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
