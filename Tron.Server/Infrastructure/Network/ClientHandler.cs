using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using Tron.Common.Extensions;
using Tron.Common.Resources;
using Tron.Server.Domain;

namespace Tron.Server.Infrastructure.Network
{
    internal class ClientHandler
    {
        private Socket _server;
        private EndPoint _client;
        private IDbManager _dbManager;

        internal ClientHandler(Connection connection, IDbManager dbManager)
        {
            _server = connection.Server;
            _client = connection.Client;
            _dbManager = dbManager;
        }

        internal void Handle()
        {
            string message = _server.ReceiveFrom(ref _client);
            string[] query = message.Split('/');

            switch ((Protocol)int.Parse(query[0]))
            {
                case Protocol.Host:
                    WaitingLobby waitingLobby = new WaitingLobby(_server, _client, 0, false, 0);
                    _dbManager.AddWaitingLobby(waitingLobby);
                    break;

                case Protocol.Guest:
                    List<WaitingLobby> waitingLobbies = _dbManager.GetWaitingLobbies();
                    if (waitingLobbies != null)
                    {
                        _server.SendTo(_client, waitingLobbies.ToString()!);
                    }
                    else
                    {
                        _server.SendTo(_client, Protocol.NoLobbies.ToString());
                    }
                    break;

                default:
                    _server.SendTo(_client, Protocol.Resend.ToString());
                    break;
            }
        }
    }
}
