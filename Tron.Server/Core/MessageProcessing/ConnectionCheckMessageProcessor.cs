using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class ConnectionCheckMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
