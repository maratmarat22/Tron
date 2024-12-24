using Microsoft.Data.Sqlite;
using System.Net;
using Tron.Server.Core.Domain.Entities;

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

        private string SerializeIPEndPoint(IPEndPoint point)
        {
            return point.Address.ToString() + ':' + point.Port.ToString();
        }

        private IPEndPoint DeserializeIPEndPoint(string point)
        {
            string[] segments = point.Split(":");
            return new IPEndPoint(IPAddress.Parse(segments[0]), int.Parse(segments[1]));
        }

        private int SerializeBool(bool value)
        {
            return value ? 1 : 0;
        }

        private bool DeserializeBool(int value)
        {
            return value == 1;
        }
    }
}
