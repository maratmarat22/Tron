using Tron.Common.Messages.General;
using Tron.Common.Messages.PreGame;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core.MessageProcessing
{
    internal class GetLobbiesMessageProcessor : ILobbyMessageProcessor
    {
        private readonly IDbQueryProcessor _queryProcessor;
        private readonly UdpUnicaster _unicaster;

        internal GetLobbiesMessageProcessor(IDbQueryProcessor queryProcessor, UdpUnicaster unicaster)
        {
            _queryProcessor = queryProcessor;
            _unicaster = unicaster;
        }

        public (Proceed, Lobby?) Process(Message message)
        {
            GetLobbiesMessage msg = (GetLobbiesMessage)message;
            int maxPlayers1 = msg.MaxPlayers;
            bool @private1 = msg.Private;

            string[] lobbies = _queryProcessor.ReadLobbies();
            ReturnLobbiesMessage response = new(Header.ReturnLobbies, [.. lobbies]);
            _unicaster.Send(response);

            return (Proceed.False, null);
        }
    }
}
