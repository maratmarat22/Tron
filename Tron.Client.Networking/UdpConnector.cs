using System.Net;
using System.Net.Sockets;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Client.Networking
{
    public class UdpConnector
    {
        private EndPoint _acceptor;

        public UdpConnector(IPAddress address, int port)
        {
            _acceptor = new IPEndPoint(address, port);
        }

        public UdpUnicaster? TryConnect(AuthentificationMessage auth)
        {
            Socket local = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            local.ReceiveTimeout = 3000;
            local.SendTimeout = 3000;
            
            if (local.TryBind(IPAddress.Loopback, 5000))
            {
                try
                {
                    local.SafeSendTo(auth, _acceptor);
                }
                catch (SocketException)
                {
                    local.Close();
                    local.Dispose();
                    return null;
                }

                IPEndPointMessage redirect = (IPEndPointMessage)local.SafeReceiveFrom(ref _acceptor, Header.REDIRECT);
                IPEndPoint remote = new IPEndPoint(redirect.Address, redirect.Port);

                local.SafeSendTo(new Message(Header.CONNECT), remote);

                return new UdpUnicaster(local, remote);
            }

            return null;
        }
    }
}
