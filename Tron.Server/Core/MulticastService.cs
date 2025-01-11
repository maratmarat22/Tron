using System.Net;
using Tron.Common.Messages;
using Tron.Server.Core.MessageProcessing;
using Tron.Server.Networking;

namespace Tron.Server.Core
{
    internal class MulticastService
    {
        private Multicaster _multicaster;

        private Dictionary<string, string?> _state;

        private MessageProcessorPool _pool;

        internal MulticastService(Multicaster multicaster, Dictionary<string, string?> state, MessageProcessorPool pool)
        {
            _multicaster = multicaster;
            _state = state;
            _pool = pool;
        }

        internal void Run()
        {
            while (true)
            {
                (Message? message, EndPoint? sender) = _multicaster.Receive();

                if (message != null)
                {
                    if (message.Header == Header.Acknowledge)
                    { }
                    
                    IMessageProcessor processor = _pool.Acquire(message.Header);
                    Message response = processor.Process(message, _state, _multicaster);

                    _multicaster.SendTo(response, sender!);

                    if (message.Header == Header.DeleteLobby)
                    {
                        break;
                    }
                    if (message.Header == Header.LeaveLobby)
                    {
                    
                    }
                }
                else
                {
                    throw new NotFiniteNumberException();
                }
            }
        }
    }
}
