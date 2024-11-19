using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using Tron.Common.Extensions;
using Tron.Server.Domain;

namespace Tron.Server.Infrastructure.Network
{
    internal class ClientHandler
    {
        private Socket _server;
        private EndPoint _client;
        private IDbManager _dbManager;

        internal ClientHandler(Socket server, EndPoint client, IDbManager dbManager)
        {
            _server = server;
            _client = client;
            _dbManager = dbManager;
        }

        internal void Handle()
        {
            string message = _server.ReceiveFrom(ref _client);
            string[] query = message.Split('/');

            switch (query[0])
            {
                case "Host":
                    WaitingLobby waitingLobby = new WaitingLobby(_server, _client, 0, false, 0);
                    _dbManager.AddWaitingLobby(waitingLobby);
                    break;

                case "Guest":
                    List<WaitingLobby> waitingLobbies = _dbManager.GetWaitingLobbies();
                    break;

                default:
                    //throw new придумать эксепшен
                    break;
            }
        }
    }
}
