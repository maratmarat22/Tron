using Microsoft.Data.Sqlite;

namespace Tron.Server.Infrastructure.Persistence
{
    internal class SQLiteDbManager : IDbManager
    {
        private string _connectionString;

        internal SQLiteDbManager(string dbPath)
        {
            _connectionString = "Data Source=" + dbPath + ";Version=3";
        }

        internal List<string> GetLobbies()
        {
            List<string> lobbies = [];

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                string query = $"SELECT * FROM lobbies";

                connection.Open();

                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lobbies.Add(reader.GetString(1));
                }
            }

            return lobbies;
        }
    }
}
