using Tron.Common.Messages;

namespace Tron.Server.Core.MessageProcessing
{
    internal class LeaveLobbyMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            state["GuestName"] = null;
            state["GuestReady"] = "False";

            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
