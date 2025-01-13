using System.Text.Json;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class SessionStateMessageProcessor : IMessageProcessor
    {
        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            if (request.Payload.Length != 0)
            {
                string[] changes = request.Payload[0].Split(',');

                foreach (var change in changes)
                {
                    string[] pair = change.Split(':');
                    state![pair[0]] = pair[1];
                }
            }

            return new Message(Header.Ok, [request.Header.ToString(), JsonSerializer.Serialize(state)]);
        }
    }
}
