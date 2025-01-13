using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server.Networking
{
    internal class Acceptor
    {
        private readonly Socket _acceptor;
        private readonly IPAddress _address;
        private int _availablePort;

        private readonly IDbQueryProcessor _queryProcessor;

        internal Acceptor(IPEndPoint point, IDbQueryProcessor queryProcessor)
        {
            _acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _acceptor.Bind(point);

            Console.WriteLine($"Acceptor: listening on port {point.Port}");

            _address = point.Address;
            _availablePort = point.Port + 1;

            _queryProcessor = queryProcessor;
        }

        internal Unicaster? Accept()
        {
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            Message? authRequest = _acceptor.TryReceiveFrom(ref remote);

            if (authRequest == null) return null;

            bool authorized = authRequest.Header switch
            {
                Header.Register => _queryProcessor.Register(authRequest.Payload[0]),
                Header.LogIn => _queryProcessor.LogIn(authRequest.Payload[0]),
                _ => false
            };

            if (!authorized) return null;

            Unicaster? unicaster = TryRedirect(remote, authRequest.Header);

            if (unicaster == null)
            {
                _acceptor.TrySendTo(new Message(Header.InternalServerError, []), remote);
            }

            return unicaster;
        }

        private Unicaster? TryRedirect(EndPoint remote, Header authHeader)
        {
            Socket local = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            if (!local.TryBind(_address, _availablePort)) return null;

            Console.WriteLine($"Server socket: listening on port {_availablePort}");

            IPEndPoint localEndPoint = (local.LocalEndPoint as IPEndPoint)!;
            _availablePort = localEndPoint.Port + 1;

            if (!_acceptor.TrySendTo(new Message(Header.Ok, [authHeader.ToString(), localEndPoint.ToString()]), remote)) return null;

            Message? connect = local.TryReceiveFrom(ref remote);

            if (connect == null) return null;

            Console.WriteLine($"{remote}: connected to {localEndPoint.Port}");

            return new Unicaster(local, (IPEndPoint)remote);
        }
    }
}
