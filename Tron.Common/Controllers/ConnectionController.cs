using Tron.Common.Domain;
using Tron.Common.Extensions;

namespace Tron.Common.Controllers
{
    public class ConnectionController
    {
        private Connection _connection;

        public ConnectionController(Connection connection)
        {
            _connection = connection;
        }

        public void Send(string message)
        {
            _connection.Local.SendTo(_connection.Remote, message);
        }

        public Message Receive()
        {
            return new Message(_connection.Local.ReceiveFrom(_connection.Remote));
        }
    }
}
