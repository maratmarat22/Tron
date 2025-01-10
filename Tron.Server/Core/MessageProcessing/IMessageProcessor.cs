using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal interface IMessageProcessor
    {
        internal Message Process(Message message, Dictionary<string, string?> state, object caster);
    }
}
