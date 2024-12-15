using Microsoft.Data.Sqlite;
using Tron.Server.Core.Domain.Entities;

namespace Tron.Server.Persistence.QueryProcessing
{
    internal class SQLiteQueryProcessor : IDbQueryProcessor
    {
        private string _connectionString;

        internal SQLiteQueryProcessor(string path)
        {
            _connectionString = "Data Source=" + path + ";Version=3";
        }

        public int CreateLobby(Lobby lobby)
        {
            //using (SqliteConnection connection = new(_connectionString))
            //{
            //    var query = $"INSERT INTO lobbies (id, master, host, players, max_players, private, password) values (@id, @master, @host, @players, @max_players, @private, @password)";
            //    connection.Open();

            //    SqliteCommand command = new SqliteCommand(query, connection);

            //    using (SqliteTransaction transaction = connection.BeginTransaction())
            //    {
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
            //        command.Parameters.AddWithValue();
                    
            //        command.ExecuteNonQuery();
            //        command.Parameters.Clear();

            //        transaction.Commit();
            //    }

            //    connection.Close();
            //}

            return 0;
        }

        public string[] ReadLobbies()
        {
            return new string[0];
        }

        public void UpdateLobby(Lobby lobby)
        {

        }

        public void DeleteLobby(Lobby lobby)
        {

        }

        public string[] ReadTopTen()
        {
            return new string[0];
        }

        public void UpdateTopTen(Lobby lobby)
        {

        }
    }
}
