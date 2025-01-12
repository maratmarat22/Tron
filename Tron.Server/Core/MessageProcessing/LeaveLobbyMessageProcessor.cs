using System.Net;
using Tron.Common.Messages;
using Tron.Server.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class LeaveLobbyMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            ((Multicaster)caster).RemoveRemote(IPEndPoint.Parse(message.Payload[0]));
            state["GuestName"] = null;
            state["GuestReady"] = "False";

            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
