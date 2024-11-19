using Tron.Common.Infrastructure;
using Tron.Server.Infrastructure.Network;

namespace Tron.Server
{
    internal class Application
    {
        internal void Run()
        {
            FileReader reader = new FileReader();
            (string, int) serverSocket = reader.ReadSocket(@"../../../../Tron.Common/Resources/ServerSocket.txt");
            //Console.WriteLine(serverSocket);

            UdpServer server = new UdpServer(serverSocket);
            server.Launch();

            while (true)
            {
                ClientHandler handler = server.Listen();
                _ = Task.Run(() => handler.Handle());
            }
        }
    }
}
