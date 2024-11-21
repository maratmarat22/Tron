using Tron.Common.Extensions;
using Tron.Common.Domain;
using Tron.Server.Domain;

namespace Tron.Server.Controllers
{
    internal class MulticonnectionController
    {
        private Multiconnection _connection;

        internal MulticonnectionController(Multiconnection connection)
        {
            _connection = connection;
        }

        internal void Send(string message, int id)
        {
            Remote target = _connection.Remotes.Find(r => r.Id == id);
            _connection.Local.SendTo(target.EndPoint, message);
        }

        internal Message Receive()
        {
            return new Message(_connection.Local.ReceiveFrom(_connection.Remotes[0].EndPoint));
        }

        internal void Broadcast(string message)
        {
            foreach (var remote in _connection.Remotes)
            {
                _connection.Local.SendTo(remote.EndPoint, message);
            }
        }
    }
}
