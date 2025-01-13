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

        public bool Register(string username)
        {
            using SqliteConnection connection = new(_connectionString);
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

        public bool LogIn(string username)
        {
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            string query = $"SELECT COUNT(*) FROM players WHERE username = @username";

            using SqliteCommand command = new(query, connection);
            
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

        public Dictionary<string, int> ReadTopTen()
        {
            Dictionary<string, int> topTen = [];
            
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            string query = $"SELECT username, score FROM players ORDER BY score DESC LIMIT 10";

            using SqliteCommand command = new(query, connection);
            using SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string username = reader.GetString(0);
                int score = reader.GetInt32(1);

                topTen.Add(username, score);
            }

            return topTen;
        }

        public bool AddToScore(string username, int addition)
        {
            using SqliteConnection connection = new(_connectionString);
            connection.Open();

            string query = $"UPDATE players SET score = score + @addition WHERE username = @username";

            using SqliteCommand command = new(query, connection);
            try
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@addition", addition);
                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
