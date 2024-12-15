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
            ConnectionEstablished = false;
        }
        
        public void ConnectToServer()
        {
            SocketReader reader = new SocketReader();
            (string address, int port) acceptor = reader.Read(@"../../../../Tron.Common/Config/Data/ServerSocket.txt");
            UdpConnector connector = new UdpConnector(acceptor);
            _unicaster = connector.Connect();
            ConnectionEstablished = true;
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
