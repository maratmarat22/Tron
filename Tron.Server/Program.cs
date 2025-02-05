﻿using System.Net;
using Tron.Common.Persistence;
using Tron.Server.Core;
using Tron.Server.Persistence.QueryProcessing;

namespace Tron.Server
{
    internal class Program
    {
        static void Main()
        {
            (string address, int port) = FileProcessor.ReadSocket(@"../../../../Tron.Common/Persistence/ServerSocket.txt");

            IDbQueryProcessor queryProcessor = new SQLiteQueryProcessor(@"../../../Persistence/Data/tron.db");

            Application application = new(queryProcessor, IPAddress.Parse(address), port);
            application.Run();
        }
    }
}
