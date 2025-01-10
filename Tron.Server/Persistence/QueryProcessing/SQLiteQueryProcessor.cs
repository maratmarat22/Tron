using Microsoft.Data.Sqlite;

namespace Tron.Server.Persistence.QueryProcessing
{
    internal class SQLiteQueryProcessor : IDbQueryProcessor
    {
        private readonly string _connectionString;

        internal SQLiteQueryProcessor(string path)
        {
            _connectionString = "Data Source=" + path + ";";
        }

        public bool TryRegister(string username)
        {
            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                string query = $"SELECT COUNT(*) FROM players WHERE username = @username";

                using (SqliteCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    if (Convert.ToInt32(command.ExecuteScalar()) > 0)
                    {
                        return false;
                    }
                }

                query = $"INSERT INTO players (username) VALUES (@username)";

                using (SqliteCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.ExecuteNonQuery();

                    return true;
                }
            }
        }

        public bool TryLogIn(string username)
        {
            using (SqliteConnection connection = new(_connectionString))
            {
                connection.Open();

                string query = $"SELECT COUNT(*) FROM players WHERE username = @username";

                using (SqliteCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    if (Convert.ToInt32(command.ExecuteScalar()) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public string[] ReadTopTen()
        {
            return new string[0];
        }

        public void UpdatePlayer()
        {

        }
    }
}
