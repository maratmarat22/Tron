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

        internal Unicaster? Unicaster { get; set; }

        private Connector _connector;

        internal bool Volume { get; set; }

        internal string? Username { get; set; }

        private Unicaster? _gameUnicaster;

        public bool Connected { get; set; }

        internal App()
        {
            _processor = new();
            _acceptor = _processor.ReadSocket(@"../../../../Tron.Common/Persistence/Data/ServerSocket.txt");
            _connector = new Connector(IPAddress.Parse(_acceptor.address), _acceptor.port);

            Username = _processor.TryReadUsername(@"../../../../Tron.Client.Application/Persistence/Username.txt");

            Volume = true;
            Connected = false;
        }

        internal void SaveUsername()
        {
            _processor.SaveUsername(@"../../../../Tron.Client.Application/Persistence/Username.txt", Username!);
        }

        internal bool TrySendToServer(Message message)
        {
            if (message.Header == Header.Register || message.Header == Header.LogIn)
            {
                Unicaster = _connector.TryConnect(message);

                return Connected = Unicaster != null;
            }
            
            Unicaster!.Send(message);

            Message? response = Unicaster.Receive();

            return response != null &&
                response.Header == Header.Acknowledge &&
                response.Payload[0] == message.Header.ToString();
        }

        internal string[]? TrySendToServerAndGetPayload(Message message)
        {
            Unicaster!.Send(message);

            Message? response = Unicaster.Receive();

            return response?.Payload;
        }

        internal bool TrySendToMaster(Message message)
        {
            if (message.Header == Header.JoinLobby)
            {
                _gameUnicaster = new Unicaster(Unicaster!.Local, IPEndPoint.Parse(message.Payload[0]));
            }
            else if (message.Header == Header.CreateLobby)
            {
                _gameUnicaster = Unicaster;
            }

            _gameUnicaster!.Send(message);

            Message? response = _gameUnicaster.Receive();

            return response != null &&
                response.Header == Header.Acknowledge &&
                response.Payload[0] == message.Header.ToString();
        }

        internal string[]? TrySendToMasterAndGetPayload(Message message)
        {
            if (message.Header == Header.JoinLobby)
            {
                _gameUnicaster = new Unicaster(Unicaster!.Local, IPEndPoint.Parse(message.Payload[0]));
            }
            else if (message.Header == Header.CreateLobby)
            {
                _gameUnicaster = Unicaster;
            }

            _gameUnicaster!.Send(message);

            Message? response = _gameUnicaster.Receive();

            return response?.Payload;
        }
    }
}
