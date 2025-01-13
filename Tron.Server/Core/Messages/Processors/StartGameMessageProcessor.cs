using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class StartGameMessageProcessor : IMessageProcessor
    {
        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            long startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 5;

            state!["GameStarted"] = "True";
            state["StartTime"] = startTime.ToString();

            return new Message(Header.Ok, [request.Header.ToString(), startTime.ToString()]);
        }
    }
}
