using Tron.Common.Domain.Messages;

namespace Tron.Common.Infrastructure.Networking
{
    public class ConnectionController
    {
        public Connection Connection { get; private set; }

        public ConnectionController(Connection connection)
        {
            Connection = connection;
        }

        public void Send(Message message)
        {
            Connection.Local.SendTo(Connection.Remote, message.ToString()!);
        }

        public Message Receive()
        {
            return new Message(Connection.Local.ReceiveFrom(Connection.Remote));
        }
    }
}
