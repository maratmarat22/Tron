using System.Net;
using System.Net.Sockets;
using Tron.Common.Networking;

namespace Tron.Server.Networking
{
    internal class UdpAcceptor
    {
        private readonly UdpClient _acceptor;
        private readonly IPAddress _address;
        private int _availablePort;

        internal UdpAcceptor(IPEndPoint point)
        {
            _acceptor = new UdpClient(point);
            _address = point.Address;
            _availablePort = point.Port + 1;
        }

        internal UdpUnicaster Accept()
        {
            IPEndPoint client = new(IPAddress.Any, 0);
            _ = _acceptor.Receive(ref client);

            UdpClient server = ProvideUdpClient();

            NotifyClient(server, client);

            return new UdpUnicaster(server, client);
        }

        private UdpClient ProvideUdpClient()
        {
            UdpClient? server = null;
            bool portAvailable = false;

            while (!portAvailable)
            {
                try
                {
                    IPEndPoint point = new(_address, _availablePort);
                    server = new UdpClient(point);
                    portAvailable = true;
                }
                catch (SocketException)
                {
                    ++_availablePort;
                }

                ++_availablePort;
            }

            return server!;
        }

        private void NotifyClient(UdpClient server, IPEndPoint client)
        {
            server.SendString(client, server.Client.LocalEndPoint!.ToString()!);
        }
    }
}
