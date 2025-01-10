using System.Text.Json;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class SessionStateMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, object caster)
        {
            if (message.Payload.Length == 0)
            {
                return new Message(Header.SessionState, [JsonSerializer.Serialize(state)]);
            }
            else
            {
                string[] changes = message.Payload[0].Split(',');

                foreach (string change in changes)
                {
                    string[] keyValuePair = change.Split(':');
                    state[keyValuePair[0]] = keyValuePair[1];
                }
            }

            return new Message(Header.SessionState, [JsonSerializer.Serialize(state)]);
        }
    }
}
