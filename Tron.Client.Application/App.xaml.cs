using System.Text.Json;
using System.Net;
using Tron.Common.Persistence;
using Tron.Common.Networking;
using Tron.Common.Entities;
using Tron.Common.Messages;
using Tron.Client.Networking;

namespace Tron.Client.Application
{
    public partial class App : System.Windows.Application
    {
        private (string address, int port) _acceptor;

        private readonly Connector _connector;

        private Unicaster? _unicaster;

        private Unicaster? _gameUnicaster;

        internal string? Username { get; set; }

        //internal bool Volume { get; set; }

        internal App()
        {
            _acceptor = FileProcessor.ReadSocket(@"../../../../Tron.Common/Persistence/ServerSocket.txt");
            _connector = new Connector(IPAddress.Parse(_acceptor.address), _acceptor.port);

            Username = FileProcessor.ReadUsername(@"../../../../Tron.Client.Application/Resources/Username.txt");
            //Volume = true;
        }

        internal void WriteUsername() => FileProcessor.WriteUsername(@"../../../../Tron.Client.Application/Resources/Username.txt", Username!);

        internal bool Connect(Header connectionHeader, string username)
        {
            Message request = new(connectionHeader, [username]);
            
            _unicaster = _connector.TryConnect(request);
            
            return _unicaster != null;
        }

        internal bool CheckConnection()
        {
            if (_unicaster == null) return false;

            return AckRequest(new Message(Header.ConnectionCheck, []), _unicaster);
        }

        internal bool CreateLobby(string username, bool isPrivate, string password)
        {
            Message request = new(Header.CreateLobby, [username, isPrivate.ToString(), password]);

            if (!AckRequest(request, _unicaster!)) return false;

            _gameUnicaster = _unicaster;
            return true;
        }

        internal bool JoinLobby(string master)
        {
            Message request = new(Header.JoinLobby, [master]);

            if (!AckRequest(request, _unicaster!)) return false;

            _gameUnicaster = new Unicaster(_unicaster!.Local, IPEndPoint.Parse(master));
            return true;
        }

        internal List<Lobby>? FetchLobbies()
        {
            Message request = new(Header.FetchLobbies, []);

            _unicaster!.Send(request);

            Message? response = _unicaster.Receive();

            if (response == null) return null;

            return JsonSerializer.Deserialize<List<Lobby>>(response.Payload[1]);
        }

        internal bool DeleteLobby() => AckRequest(new Message(Header.DeleteLobby, []), _gameUnicaster!);

        internal bool LeaveLobby() => AckRequest(new Message(Header.LeaveLobby, []), _gameUnicaster!);

        internal Dictionary<string, int>? FetchTopTen()
        {
            Message request = new(Header.FetchTopTen, []);
            _unicaster!.Send(request);
            
            Message? response = _unicaster.Receive();

            if (response == null) return null;

            return JsonSerializer.Deserialize<Dictionary<string, int>>(response.Payload[1]);
        }

        internal bool StartGame() => AckRequest(new Message(Header.StartGame, []), _gameUnicaster!);

        internal void AwaitStart()
        {
            _gameUnicaster!.Local.ReceiveTimeout = (int)Networking.Timeout.Awaiting;
            _gameUnicaster.Receive();
            _gameUnicaster!.Local.ReceiveTimeout = (int)Networking.Timeout.Common;
        }

        internal Dictionary<string, string>? RefreshSessionState(string[] refreshArgs)
        {
            _gameUnicaster!.Send(new Message(Header.SessionState, [.. refreshArgs]));
            Message? response = _gameUnicaster.Receive();

            if (response == null || response.Payload == null) return null;

            return JsonSerializer.Deserialize<Dictionary<string, string?>>(response.Payload[1])!;
        }

        internal Direction[]? FetchDirections()
        {
            _gameUnicaster!.Send(new Message(Header.FetchDirections, []));
            Message? response = _gameUnicaster.Receive();

            Direction[] directions;

            if (response == null) return null;

            try
            {
                directions =
                [
                    (Direction)Enum.Parse(typeof(Direction), response.Payload[1]),
                    (Direction)Enum.Parse(typeof(Direction), response.Payload[2]),
                ];
            }
            catch (Exception)
            {
                return null;
            }

            return directions;
        }

        internal bool AddScore(string username, int addition)
        {
            Message request = new(Header.AddScore, [username, addition.ToString()]);
            
            return AckRequest(request, _gameUnicaster!);
        }

        private static bool AckRequest(Message request, Unicaster unicaster)
        {
            unicaster!.Send(request);

            Message? response = unicaster.Receive();

            return IsValidResponse(response, request.Header);
        }

        private static bool IsValidResponse(Message? response, Header requestHeader)
        {
            return response != null && response.Header == Header.Ok && response.Payload[0] == requestHeader.ToString();
        }
    }
}
