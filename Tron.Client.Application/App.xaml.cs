using System.Net;
using System.Text.Json;
using Tron.Client.Networking;
using Tron.Common.Entities;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Common.Persistence;

namespace Tron.Client.Application
{
    public partial class App : System.Windows.Application
    {
        private readonly FileProcessor _processor;

        private (string address, int port) _acceptor;

        private readonly Connector _connector;

        private Unicaster? _unicaster;

        private Unicaster? _gameUnicaster;

        internal string? Username { get; set; }

        internal bool Volume { get; set; }

        internal App()
        {
            _processor = new();
            _acceptor = _processor.ReadSocket(@"../../../../Tron.Common/Persistence/ServerSocket.txt");
            _connector = new Connector(IPAddress.Parse(_acceptor.address), _acceptor.port);

            Username = _processor.ReadUsername(@"../../../../Tron.Client.Application/Resources/Username.txt");
            Volume = true;
        }

        internal void LogUsername() => _processor.WriteUsername(@"../../../../Tron.Client.Application/Resources/Username.txt", Username!);

        internal bool TryConnect(Header connectionHeader, string username)
        {
            Message request = new(connectionHeader, [username]);
            _unicaster = _connector.TryConnect(request);
            return _unicaster != null;
        }

        internal bool TryCreateLobby(string username, bool isPrivate, string password)
        {
            Message request = new(Header.CreateLobby, [username, isPrivate.ToString(), password]);
            
            if (AckRequest(request, Point.Server))
            {
                _gameUnicaster = _unicaster;
                return true;
            }

            return false;
        }

        internal bool TryJoinLobby(string master)
        {
            Message request = new(Header.JoinLobby, [master]);
            
            if (AckRequest(request, Point.Server))
            {
                _gameUnicaster = new Unicaster(_unicaster!.Local, IPEndPoint.Parse(master));
                return true;
            }

            return false;
        }

        internal List<Lobby>? FetchLobbies()
        {
            _unicaster!.Send(new Message(Header.FetchLobbies, []));
            Message? response = _unicaster.Receive();

            if (response != null)
            {
                return JsonSerializer.Deserialize<List<Lobby>>(response.Payload[1]);
            }

            return null;
        }

        internal bool StartGame()
        {
            _gameUnicaster!.Send(new Message(Header.StartGame, []));
            Message? response = _gameUnicaster.Receive();

            return IsValidResponse(response, Header.StartGame);
        }

        private bool IsValidResponse(Message? response, Header requestHeader)
        {
            return response != null && response.Header == Header.Ok && response.Payload[0] == requestHeader.ToString();
        }

        internal bool CheckConnection()
        {
            if (_unicaster == null) return false;
            
            _unicaster!.Send(new Message(Header.ConnectionCheck, []));
            Message? response = _unicaster.Receive();

            return IsValidResponse(response, Header.ConnectionCheck);
        }

        internal Dictionary<string, string?>? RefreshSessionState(string[] refreshArgs)
        {
            _gameUnicaster!.Send(new Message(Header.SessionState, [.. refreshArgs]));
            Message? response = _gameUnicaster.Receive();

            if (response != null && response.Payload != null)
            {
                return JsonSerializer.Deserialize<Dictionary<string, string?>>(response.Payload[1])!;
            }
            else
            {
                return null;
            }
        }

        private bool AckRequest(Message request, Point point)
        {
            Unicaster unicaster = point == Point.Server ? _unicaster : _gameUnicaster;

            unicaster!.Send(request);
            Message? response = unicaster.Receive();

            return IsValidResponse(response, request.Header);
        }

        internal Direction[] FetchDirections()
        {
            _gameUnicaster!.Send(new Message(Header.FetchDirections, []));
            Message? response = _gameUnicaster.Receive();

            Direction[] directions =
            [
                (Direction)Enum.Parse(typeof(Direction), response.Payload[0]),
                (Direction)Enum.Parse(typeof(Direction), response.Payload[1]),
            ];
            return directions;
        }

        internal bool DeleteLobby() => AckRequest(new Message(Header.DeleteLobby, []), Point.Master);

        internal bool LeaveLobby() => AckRequest(new Message(Header.LeaveLobby, [_gameUnicaster!.Local.ToString()!]), Point.Master);
    }
}
