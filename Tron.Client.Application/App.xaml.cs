using System.Net;
using Tron.Client.Networking;
using Tron.Common.Config.Utilities;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Client.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private FileProcessor _processor;

        private (string address, int port) _acceptor;
        
        private UdpUnicaster? _unicaster;
        private UdpConnector _connector;

        internal bool Volume { get; set; }

        internal bool Connected { get; private set; }

        internal string? Username { get; set; }

        internal App()
        {
            _processor = new();
            _acceptor = _processor.ReadSocket(@"../../../../Tron.Common/Persistence/Data/ServerSocket.txt");
            _connector = new UdpConnector(IPAddress.Parse(_acceptor.address), _acceptor.port);

            Username = _processor.TryReadUsername(@"../../../../Tron.Client.Application/Persistence/Username.txt");

            Volume = true;
            Connected = false;
        }

        internal void LogUsername()
        {
            _processor.LogUsername(@"../../../../Tron.Client.Application/Persistence/Username.txt", Username!);
        }

        internal bool TryRegister(string username)
        {
            AuthentificationMessage auth = new(Header.REGISTER, username);

            _unicaster = _connector.TryConnect(auth);

            bool connected = _unicaster != null;
            Connected = connected;
            return connected;
        }

        internal bool TryLogIn()
        {
            AuthentificationMessage auth = new(Header.LOGIN, Username!);

            _unicaster = _connector.TryConnect(auth);

            bool connected = _unicaster != null;
            Connected = connected;
            return connected;
        }

        internal bool TryCreateLobby(bool isPrivate, string password)
        {
            if (!Connected)
            {
                return false;
            }
            else
            {
                CreateLobbyMessage create = new(Username!, isPrivate, password);
                _unicaster!.Send(create);
                return true;
            }
        }
    }
}
