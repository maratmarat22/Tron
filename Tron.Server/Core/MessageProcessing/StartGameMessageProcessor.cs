using Tron.Common.Messages;

namespace Tron.Server.Core.MessageProcessing
{
    internal class StartGameMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            long startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 5;

            state["GameStarted"] = "True";
            state["StartTime"] = startTime.ToString();

            return new Message(Header.Acknowledge, [startTime.ToString()]);
        }
    }
}
