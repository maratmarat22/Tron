using System.Net;
using Tron.Common.Infrastructure.Networking;

namespace Tron.Server.Infrastructure.Networking
{
    internal class MulticonnectionController
    {
        private Multiconnection _multiconnection;

        internal MulticonnectionController(Multiconnection multiconnection)
        {
            _multiconnection = multiconnection;

        }

        //internal Message Receive()
        //{
        //    return new Message(_multiconnection.Local.ReceiveFrom(_multiconnection.Remotes[0].EndPoint));
        //}

        internal void Broadcast(string message)
        {
            foreach (var connection in _multiconnection.Connections)
            {
                _multiconnection.Local.SendTo(connection.Remote, message);
            }
        }

        internal void AddConnection(EndPoint remote)
        {
            _multiconnection.AddConnection(remote);
        }

        internal void RemoveConnection(EndPoint remote)
        {
            _multiconnection.RemoveConnection(remote);
        }
    }
}
