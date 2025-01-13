using Tron.Common.Messages;
using Tron.Server.Networking;
using Tron.Common.Networking;
using System.Net;

namespace Tron.Server.Core.Messages.Processors
{
    internal class JoinLobbyMessageProcessor : IMessageProcessor
    {
        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            string remoteToAdd = unicaster!.Remote.ToString()!;
            EndPoint master = IPEndPoint.Parse(request.Payload[0]);

            Message subRequest = new(Header.AddRemote, [remoteToAdd]);

            if (unicaster.Local.TrySendTo(subRequest, master))
            {
                Message? subResponse = unicaster.Local.TryReceiveFrom(master);

                if (subResponse != null && subResponse.Header == Header.Ok && subResponse.Payload[0] == subRequest.Header.ToString())
                {
                    return new Message(Header.Ok, [request.Header.ToString()]);
                }
            }

            return new Message(Header.Nok, [request.Header.ToString()]);
        }
    }
}
