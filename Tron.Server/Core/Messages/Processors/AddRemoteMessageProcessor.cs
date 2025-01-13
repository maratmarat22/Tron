using System.Net;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class AddRemoteMessageProcessor : IMessageProcessor
    {
        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            var remoteToAdd = IPEndPoint.Parse(request.Payload[0]);

            bool succeed = multicaster!.AddRemote(remoteToAdd);
            Message response;

            if (succeed)
            {
                response = new(Header.Ok, [request.Header.ToString()]);
            }
            else
            {
                response = new(Header.Nok, [request.Header.ToString()]);
            }

            return response;
        }
    }
}
