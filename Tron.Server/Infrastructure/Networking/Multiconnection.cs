using System.Net;
using System.Net.Sockets;
using Tron.Common.Infrastructure.Networking;

namespace Tron.Server.Infrastructure.Networking
{
    internal struct Multiconnection
    {
        public List<Connection> Connections { get; private set; }

        public Socket Local { get; private set; }

        public int MaxConnections { get; private set; }

        internal Multiconnection(Connection connection, int maxConnections)
        {
            Local = connection.Local;
            Connections = new List<Connection>();
            Connections.Add(connection);
            MaxConnections = maxConnections;
        }

        internal void AddConnection(EndPoint remote)
        {
            if (Connections.Count + 1 <= MaxConnections)
            {
                Connection connection = new Connection(Local, remote);
                Connections.Add(connection);
            }
        }

        internal void RemoveConnection(EndPoint remote)
        {

        }
    }
}
