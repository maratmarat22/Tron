using System.Net;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Core.Messages;
using Tron.Server.Core.Messages.Processors;
using Tron.Server.Networking;

namespace Tron.Server.Core
{
    internal class ClientService
    {
        private readonly Unicaster _unicaster;
        private readonly Multicaster _multicaster;
        private readonly Dictionary<string, string> _state;
        private readonly MessageProcessorPool _pool;

        internal ClientService(Unicaster unicaster, MessageProcessorPool pool)
        {
            _unicaster = unicaster;
            _multicaster = new Multicaster(_unicaster.Local, _unicaster.Remote);

            var serverEndPoint = _unicaster.Local.LocalEndPoint!.ToString();

            _state = new()
            {
                { "ServerEndPoint", serverEndPoint! },
                { "HostName", "" },
                { "GuestName", "" },
                { "HostReady", "false" },
                { "GuestReady", "false" },
                { "GameStarted", "false" },
                { "HostX", "" },
                { "HostY", "" },
                { "GuestX", "" },
                { "GuestY", "" },
                { "HostDirection", "" },
                { "GuestDirection", "" },
            };

            _pool = pool;
        }

        internal void Run(bool runAsUnicaster)
        {
            while (true)
            {
                Message? request;
                EndPoint? sender = null;

                if (runAsUnicaster)
                {
                    request = _unicaster.Receive();
                }
                else
                {
                    (request, sender) = _multicaster.Receive();
                }

                if (request.Header == Header.Nok)
                { }

                if (request == null) break;

                (IMessageProcessor processor, bool stateRequired, bool unicasterRequired, bool multicasterRequired) = _pool.Acquire(request.Header);

                Message response = processor.Process(
                    request,
                    stateRequired ? _state : null,
                    unicasterRequired ? _unicaster : null,
                    multicasterRequired ? _multicaster : null
                    );

                if (runAsUnicaster)
                {
                    _unicaster.Send(response);
                }
                else
                {
                    _multicaster.SendTo(response, sender!);
                }

                if (request.Header == Header.CreateLobby)
                {
                    runAsUnicaster = false;
                }
                if (request.Header == Header.DeleteLobby)
                {
                    runAsUnicaster = true;
                }
            }
        }
    }
}
