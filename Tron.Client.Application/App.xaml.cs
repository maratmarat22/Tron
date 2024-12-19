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
        private (string address, int port) _acceptor;
        
        private int _musicVolume;
        public int MusicVolume
        { 
            get => _musicVolume;
            set
            {
                if (value >= 0 && value <= 100)
                    _musicVolume = value;
            }
        }

        private int _fxVolume;
        public int FxVolume
        {
            get => _fxVolume;
            set
            {
                if (value >= 0 && value <= 100)
                    _fxVolume = value;
            }
        }

        public bool ConnectionEstablished { get; set; }

        private UdpUnicaster? _unicaster;

        public App()
        {
            SocketReader reader = new SocketReader();
            _acceptor = reader.Read(@"../../../../Tron.Common/Persistence/Data/ServerSocket.txt");

            ConnectionEstablished = false;
        }
        
        public bool TryConnectToServer()
        {
            if (ConnectionEstablished) return true;
            else
            {
                UdpConnector connector = new UdpConnector(IPAddress.Parse(_acceptor.address), _acceptor.port);
                _unicaster = connector.TryConnect();

                ConnectionEstablished = _unicaster != null;
                return ConnectionEstablished;
            }
        }

        public void SendToServer(Message message)
        {
            _unicaster.Send(message);
        }

        public Message ReceiveFromServer(Message message)
        {
            return _unicaster.Receive();
        }
    }
}
