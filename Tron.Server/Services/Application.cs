using Tron.Common.Infrastructure;
using Tron.Server.Domain;
using Tron.Server.Infrastructure;

namespace Tron.Server.Services
{
    internal class Application
    {
        internal void Run()
        {
            FileReader reader = new FileReader();
            (string ip, int port) serverSocket;
            //bool socketResetted = false;
            //try
            //{
            serverSocket = reader.ReadSocket(@"../../../../Tron.Common/Resources/ServerSocket.txt");
            //}
            //catch
            //{
            //    //SocketResetter socketResetter = new SocketResetter();
            //    //serverSocket.ip = SocketResetter.GetCurrentIP();
            //    //serverSocket.port = SocketResetter.GetPort();
            //    //socketResetted = true;
            //}

            IDbManager dbManager = new SQLiteDbManager(@"../../../Resources/tron.db");

            UdpListener listener = new UdpListener(serverSocket);
            //if (socketResetted)
            //{
            //    listener.BroadcastNewSocket();
            //}

            while (true)
            {
                MultiConnection connection = listener.Listen(dbManager);
                ClientHandler handler = new ClientHandler(connection, dbManager);
                _ = Task.Run(() => handler.Handle());
            }
        }
    }
}
