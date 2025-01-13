using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class StartGameMessageProcessor : IMessageProcessor
    {
        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            state!["GameStarted"] = "True";

            return new Message(Header.Ok, [request.Header.ToString()]);
        }
    }
}
