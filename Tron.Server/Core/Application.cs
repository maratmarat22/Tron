﻿using System.Net;
using Tron.Common.Networking.P2P;
using Tron.Server.Networking;
using Tron.Server.Persistence.QueryHandlers;

namespace Tron.Server.Core
{
    internal class Application
    {
        private IDbQueryHandler _queryHandler;
        private IPEndPoint _point;
        private UdpAcceptor _acceptor;

        internal Application(IDbQueryHandler queryHandler, (string address, int port) socket)
        {
            _queryHandler = queryHandler;
            _point = new IPEndPoint(IPAddress.Parse(socket.address), socket.port);
            _acceptor = new UdpAcceptor(_point);
        }

        internal void Run()
        {
            while (true)
            {
                UdpConnection connection = _acceptor.Accept();
                UdpUnicaster unicaster = new UdpUnicaster(connection);
                Session session = new Session(_queryHandler, unicaster);
                Task.Run(session.Run);
            }
        }
    }
}