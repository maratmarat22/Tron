using System.Net;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;
using Tron.Server.Core.Messages;
using Tron.Common.Networking;
using Tron.Common.Entities;

namespace Tron.Server.Core
{
    internal class Application
    {
        private readonly IDbQueryProcessor _queryProcessor;
        private readonly EndPoint _point;
        private readonly Acceptor _acceptor;

        private readonly List<Lobby> _lobbies;
        private readonly MessageProcessorPool _pool;

        internal Application(IDbQueryProcessor queryProcessor, IPAddress address, int port)
        {
            _queryProcessor = queryProcessor;
            _point = new IPEndPoint(address, port);
            _acceptor = new Acceptor((IPEndPoint)_point, _queryProcessor);

            _lobbies = [];
            _pool = new(_lobbies, queryProcessor);
        }

        internal void Run()
        {   
            while (true)
            {
                Unicaster? unicaster = _acceptor.Accept();

                if (unicaster != null)
                {
                    ClientService service = new(unicaster, _pool);

                    Task.Run(() => service.Run(true));
                }
            }
        }
    }
}
