using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Client.Networking
{
    public class Connector
    {
        private EndPoint _acceptor;

        public Connector(IPAddress address, int port)
        {
            _acceptor = new IPEndPoint(address, port);
        }

        public Unicaster? TryConnect(Message auth)
        {
            Socket local = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                ReceiveTimeout = 1000,
                SendTimeout = 1000
            };

            bool connected = false;

            if (local.TryBind(IPv4Provider.GetActiveIPv4Address()!, 5000))
            {
                if (local.TrySendTo(auth, _acceptor))
                {
                    Message? redirect = local.TryReceiveFrom(ref _acceptor);

                    if (redirect != null)
                    {
                        IPEndPoint? remote;

                        if (redirect.Payload.Length != 0 && IPEndPoint.TryParse(redirect.Payload[1], out remote))
                        {
                            if (local.TrySendTo(new Message(Header.Connect, []), remote))
                            {
                                connected = true;
                                return new Unicaster(local, remote);
                            }
                        }
                    }
                }

                if (!connected)
                {
                    local.Close();
                    local.Dispose();
                }
            }

            return null;
        }
    }
}
