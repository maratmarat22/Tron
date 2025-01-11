using Tron.Common.Messages;

namespace Tron.Server.Core.MessageProcessing
{
    internal class FetchDirectionsMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            return new Message(Header.Acknowledge, [state["HostDirection"]!, state["GuestDirection"]!]);
        }
    }
}
