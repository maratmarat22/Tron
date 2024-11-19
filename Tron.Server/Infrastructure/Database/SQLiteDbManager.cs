using Tron.Server.Domain;

namespace Tron.Server.Infrastructure.Database
{
    internal class SQLiteDbManager : IDbManager
    {
        private string _connectionString;

        internal SQLiteDbManager(string dbPath)
        {
            _connectionString = "Data Source=" + dbPath + ";Version=3";
        }
    }
}
