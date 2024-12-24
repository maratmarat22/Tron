using System.Text;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core.MessageProcessing
{
    internal class ReadLobbiesMessageProcessor : ILobbyMessageProcessor
    {
        private readonly UdpUnicaster _unicaster;

        private readonly List<Lobby> _lobbies;

        internal ReadLobbiesMessageProcessor(UdpUnicaster unicaster, List<Lobby> lobbies)
        {
            _unicaster = unicaster;

            _lobbies = lobbies;
        }

        public (Proceed, Lobby?) Process(Message message)
        {
            ReadLobbiesMessage concrete = (ReadLobbiesMessage)message;

            List<string> lobbies = [];

            foreach (Lobby lobby in _lobbies)
            {
                lobbies.Add(lobby.ToString()!);
            }

            ReturnLobbiesMessage @return = new(lobbies);
            _unicaster.Send(@return);

            return (Proceed.False, null);
        }
    }
}
