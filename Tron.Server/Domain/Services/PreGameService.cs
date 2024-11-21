using Tron.Server.Infrastructure.Persistence;
using Tron.Server.Infrastructure.Networking;
using Tron.Common.Infrastructure.Networking;
using Tron.Common.Domain.Messages;
using Tron.Server.Domain.Entities;

namespace Tron.Server.Domain.Services
{
    internal class PreGameService
    {
        private ConnectionController _controller;

        private IDbManager _dbManager;

        internal PreGameService(ConnectionController controller, IDbManager dbManager)
        {
            _controller = controller;
            _dbManager = dbManager;
        }

        internal void Run()
        {
            bool cancel = false;

            while (!cancel)
            {
                Message message = _controller.Receive();

                switch (message.Header)
                {
                    case Headers.Host:

                        HostMessage hostMessage = (HostMessage)message;
                        int maxPlayers = hostMessage.MaxPlayers;
                        bool @private = hostMessage.Private;
                        int password = hostMessage.Password;

                        Multiconnection multiconnection = new Multiconnection(_controller.Connection, maxPlayers);
                        Lobby lobby = new Lobby(multiconnection.Local, multiconnection.Connections[0].Remote, maxPlayers, @private, password);

                        PlayerAwaitService playerAwaitService = new PlayerAwaitService();
                        playerAwaitService.Run();

                        break;

                    case Headers.ShowLobbies:

                        List<string> lobbies = _dbManager.GetLobbies();
                        Message lobbiesMessage = new LobbiesMessage(Headers.Lobbies, lobbies);
                        _controller.Send(lobbiesMessage);

                        if (lobbies == null)
                        {
                            Message response = new Message(Headers.NoLobbies);
                            _controller.Send(response);
                        }

                        break;

                    case Headers.Guest:

                        // Вытягиваем из БД сокет мастера лобби

                        Connection connection = new Connection(master, _controller.Connection.Remote);

                        // Отправляем ему Connect

                        PlayerAwaitService playerAwaitService1 = new PlayerAwaitService();
                        playerAwaitService1.Run();

                        break;

                    case Headers.Disconnect:

                        cancel = true;

                        break;

                    default: break;
                }
            }
        }
    }
}
