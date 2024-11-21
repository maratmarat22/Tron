using System.Net;
using System.Net.Sockets;
using Tron.Common.Domain;
using Tron.Common.Extensions;
using Tron.Server.Domain;

namespace Tron.Server.Services
{
    internal class ClientHandler
    {
        private Socket _server;
        private EndPoint _client;
        private IDbManager _dbManager;

        internal ClientHandler(MultiConnection connection, IDbManager dbManager)
        {
            _server = connection.Server;
            _client = connection.Client;
            _dbManager = dbManager;
        }

        internal void Handle()
        {
            string message = _server.ReceiveFrom(ref _client);
            string[] query = message.Split('/');

            switch ((Headers)int.Parse(query[0]))
            {
                case Headers.Host:
                    {
                        Lobby waitingLobby = new Lobby(_server, _client, 0, false, 0);
                        _dbManager.AddWaitingLobby(waitingLobby);
                        PlayerAwaitService playerAwaitService = new PlayerAwaitService(waitingLobby);
                        playerAwaitService.wait();
                        break;
                    }

                case Headers.ShowLobbies:
                    {

                        List<Lobby> waitingLobbies = _dbManager.GetWaitingLobbies();
                        if (waitingLobbies != null)
                        {
                            _server.SendTo(_client, waitingLobbies.ToString()!);
                        }
                        else
                        {
                            _server.SendTo(_client, Headers.NoLobbies.ToString());
                        }
                        break;
                    }

                case Headers.Guest:
                    {
                        break;
                    }
                    

                default:
                    _server.SendTo(_client, Headers.Resend.ToString());
                    break;
            }
        }
    }
}
