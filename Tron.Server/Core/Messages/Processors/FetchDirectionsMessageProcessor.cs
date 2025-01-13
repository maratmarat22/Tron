using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class FetchDirectionsMessageProcessor : IMessageProcessor
    {
        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            var HostDirection = state!["HostDirection"];
            var GuestDirection = state["GuestDirection"];

            return new Message(Header.Ok, [request.Header.ToString(), HostDirection, GuestDirection]);
        }
    }
}
