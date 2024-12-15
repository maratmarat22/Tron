using Tron.Common.Messages;
using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal interface ILobbyMessageProcessor
    {
        internal (Proceed, Lobby?) Process(Message message);
    }
}
