using System.Text.Json;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core.Messages.Processors
{
    internal class FetchTopTenMessageProcessor : IMessageProcessor
    {
        private readonly IDbQueryProcessor _queryProcessor;

        internal FetchTopTenMessageProcessor(IDbQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            var topTen = JsonSerializer.Serialize(_queryProcessor.ReadTopTen());

            return new Message(Header.Ok, [request.Header.ToString(), topTen]);
        }
    }
}
