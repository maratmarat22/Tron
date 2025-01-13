using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Client.Networking
{
    public class Connector(IPAddress address, int port)
    {
        private EndPoint _acceptor = new IPEndPoint(address, port);

        public Unicaster? TryConnect(Message authRequest)
        {
            int timeout = (int)Timeout.@short;

            Socket local = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                ReceiveTimeout = timeout,
                SendTimeout = timeout
            };

            IPAddress? address = IPv4Provider.GetActiveIPv4Address();

            if (address == null) return null;
            if (!local.TryBind(address, 5000)) return null;
            if (!local.TrySendTo(authRequest, _acceptor)) return Disconnect(local);

            Message? authResponse = local.TryReceiveFrom(ref _acceptor);

            if (authResponse == null) return Disconnect(local);

            string serverEndPoint = authResponse.Payload[1];

            if (authResponse.Payload.Length == 0 || !IPEndPoint.TryParse(serverEndPoint, out IPEndPoint? remote)) return Disconnect(local);

            if (!local.TrySendTo(new Message(Header.Connect, []), remote)) return Disconnect(local);

            return new Unicaster(local, remote);
        }

        private Unicaster? Disconnect(Socket local)
        {
            local.Close();
            local.Dispose();

            return null;
        }
    }
}
