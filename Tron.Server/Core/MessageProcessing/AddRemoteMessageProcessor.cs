using System.Net;
using Tron.Common.Messages;
using Tron.Server.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class AddRemoteMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            try
            {
                bool added = ((Multicaster)caster).AddRemote(IPEndPoint.Parse(message.Payload[0]));
            }
            catch (Exception)
            {
                return null;
            }

            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
