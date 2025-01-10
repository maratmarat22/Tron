using System.Net;
using Tron.Common.Config.Utilities;
using Tron.Server.Core;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server
{
    internal class Program
    {
        static void Main()
        {
            FileProcessor reader = new();
            (string address, int port) = reader.ReadSocket(@"../../../../Tron.Common/Persistence/Data/ServerSocket.txt");

            IDbQueryProcessor dbQueryProcessor = new SQLiteQueryProcessor(@"../../../Persistence/Data/tron.db");

            Application application = new(dbQueryProcessor, IPAddress.Parse(address), port);
            application.Run();
        }
    }
}
