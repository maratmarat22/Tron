using System.Net;
using Tron.Common.Networking;
using Tron.Server.Core.Domain.Entities;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Core
{
    internal class Application
    {
        private readonly IDbQueryProcessor _queryProcessor;
        private readonly EndPoint _point;
        private readonly UdpAcceptor _acceptor;

        private List<Lobby> _lobbies;

        internal Application(IDbQueryProcessor queryProcessor, IPAddress address, int port)
        {
            _queryProcessor = queryProcessor;
            _point = new IPEndPoint(address, port);
            _acceptor = new UdpAcceptor((IPEndPoint)_point, _queryProcessor);

            _lobbies = [];
        }

        internal void Run()
        {   
            while (true)
            {
                UdpUnicaster unicaster = _acceptor.Accept();

                Task.Run(() =>
                {
                    ClientProcessor processor = new(unicaster, _queryProcessor, _lobbies);
                    processor.Process();
                });
            }
        }
    }
}
