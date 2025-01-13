using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;

namespace Tron.Server.Core.Messages.Processors
{
    internal interface IMessageProcessor
    {
        internal Message Process(Message request, Dictionary<string, string>? state, Unicaster? unicaster, Multicaster? multicaster);
    }
}
