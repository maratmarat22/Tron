using Tron.Common.Messages;
using Tron.Common.Entities;
using System.Text.Json;
using Tron.Server.Networking;
using Tron.Common.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal class FetchLobbiesMessageProcessor : IMessageProcessor
    {
        private readonly List<Lobby> _lobbies;

        internal FetchLobbiesMessageProcessor(List<Lobby> lobbies)
        {
            _lobbies = lobbies;
        }

        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            var lobbies = JsonSerializer.Serialize(_lobbies);

            return new Message(Header.Ok, [request.Header.ToString(), lobbies]);
        }
    }
}
