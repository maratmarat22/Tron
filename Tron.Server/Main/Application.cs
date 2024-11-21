using Tron.Common.Infrastructure.Files;
using Tron.Common.Infrastructure.Networking;
using Tron.Server.Infrastructure.Networking;
using Tron.Server.Infrastructure.Persistence;

namespace Tron.Server.Main
{
    internal class Application
    {
        internal void Run()
        {
            FileReader reader = new FileReader();
            (string ip, int port) serverSocket;
            serverSocket = reader.ReadSocket(@"../../../../Tron.Common/Resources/ServerSocket.txt");

            IDbManager dbManager = new SQLiteDbManager(@"../../../Resources/tron.db");
            Acceptor acceptor = new Acceptor(serverSocket);

            while (true)
            {
                Connection connection = acceptor.Accept();
                ConnectionController controller = new ConnectionController(connection);
                PreGameService service = new PreGameService(controller, dbManager);
                Task.Run(service.Run);
            }
        }
    }
}
