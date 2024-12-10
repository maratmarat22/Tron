using Tron.Common.Config.Utilities;
using Tron.Server.Core;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server
{
    internal class Program
    {
        static void Main()
        {
            SocketReader reader = new();
            (string address, int port) acceptor = reader.Read(@"../../../../Tron.Common/Config/Data/ServerSocket.txt");

            IDbQueryProcessor dbQueryProcessor = new SQLiteQueryProcessor(@"../../../Persistence/Data/tron.db");            

            Application application = new(dbQueryProcessor, acceptor);
            application.Run();
        }
    }
}
