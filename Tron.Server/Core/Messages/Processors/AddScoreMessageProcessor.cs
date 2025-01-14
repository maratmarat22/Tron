using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core.Messages.Processors
{
    internal class AddScoreMessageProcessor : IMessageProcessor
    {
        private readonly IDbQueryProcessor _queryProcessor;

        internal AddScoreMessageProcessor(IDbQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster)
        {
            var username = request.Payload[0];
            var score = int.Parse(request.Payload[1]);

            bool succeed = _queryProcessor.AddScore(username, score);
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
