using System.Net;
using System.Text.Json;
using Tron.Client.Networking;
using Tron.Common.Config.Utilities;
using Tron.Common.Entities;
using Tron.Common.Messages;
using Tron.Common.Networking;

namespace Tron.Client.Application
{
    public partial class App : System.Windows.Application
    {
        private FileProcessor _processor;

        private (string address, int port) _acceptor;

        private Connector _connector;

        private Unicaster? _unicaster;

        private Unicaster? _gameUnicaster;

        internal string? Username { get; set; }

        internal bool Volume { get; set; }

        internal App()
        {
            _processor = new();
            _acceptor = _processor.ReadSocket(@"../../../../Tron.Common/Persistence/Data/ServerSocket.txt");
            _connector = new Connector(IPAddress.Parse(_acceptor.address), _acceptor.port);

            Username = _processor.TryReadUsername(@"../../../../Tron.Client.Application/Persistence/Username.txt");
            Volume = true;
        }

        internal void LogUsername() => _processor.LogUsername(@"../../../../Tron.Client.Application/Persistence/Username.txt", Username!);

        internal bool TryConnect(Message message)
        {
            _unicaster = _connector.TryConnect(message);
            return _unicaster != null;
        }

        internal bool TryCreateLobby(Message message)
        {
            if (AckRequest(message, Point.Server))
            {
                _gameUnicaster = _unicaster;
                
                return true;
            }

            return false;
        }

        internal bool TryJoinLobby(Message message)
        {
            message.Payload[0] = _unicaster!.Local.LocalEndPoint!.ToString()!;
            if (AckRequest(message, Point.Server))
            {
                _gameUnicaster = new Unicaster(_unicaster!.Local, IPEndPoint.Parse(message.Payload[1]));
                
                return true;
            }

            return false;
        }

        internal bool AckRequest(Message message, Point point)
        {
            Unicaster unicaster = point == Point.Server ? _unicaster! : _gameUnicaster!;

            if (unicaster == null)
            {
                return false;
            }

            unicaster!.Send(message);
            Message? response = unicaster.Receive();

            return IsValidResponse(response, message.Header);
        }

        internal string[]? PayloadRequest(Message message, Point point)
        {
            Unicaster unicaster = point == Point.Server ? _unicaster! : _gameUnicaster!;

            unicaster!.Send(message);
            Message? response = unicaster.Receive();

            return response?.Payload;
        }

        internal bool StartGame()
        {
            _gameUnicaster!.Send(new Message(Header.StartGame, []));
            Message? response = _gameUnicaster.Receive();

            return IsValidResponse(response, Header.StartGame);
        }

        private bool IsValidResponse(Message? response, Header requestHeader)
        {
            return response != null && response.Header == Header.Acknowledge && response.Payload[0] == requestHeader.ToString();
        }
    }
}
