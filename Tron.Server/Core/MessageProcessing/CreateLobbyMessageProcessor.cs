using System.Net;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal class CreateLobbyMessageProcessor : ILobbyMessageProcessor
    {
        private readonly UdpUnicaster _unicaster;

        private List<Lobby> _lobbies;

        internal CreateLobbyMessageProcessor(UdpUnicaster unicaster, List<Lobby> lobbies)
        {
            _unicaster = unicaster;
            _lobbies = lobbies;
        }

        public (Proceed, Lobby) Process(Message message)
        {
            CreateLobbyMessage create = (CreateLobbyMessage)message;
            
            IPEndPoint master = (IPEndPoint)_unicaster.Local.LocalEndPoint!;
            Lobby lobby = new(master, create.Hostname, create.IsPrivate, create.Password);
            
            _lobbies.Add(lobby);

            return (Proceed.True, lobby);
        }
    }
}
