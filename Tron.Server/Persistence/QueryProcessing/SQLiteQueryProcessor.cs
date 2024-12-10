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

        public void UpdateTopTen(string player, int points)
        {
        
        }
    }
}
