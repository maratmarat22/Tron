using Tron.Common.Messages.General;
using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Core.MessageProcessing
{
    internal interface ILobbyMessageProcessor
    {
        internal (Proceed, Lobby?) Process(Message message);
    }
}
