namespace Tron.Server.Persistence.QueryProcessing
{
    internal interface IDbQueryProcessor
    {
        internal bool Register(string username);

        internal bool LogIn(string username);

        internal Dictionary<string, int> ReadTopTen();

        internal bool AddToScore(string username, int addition);
    }
}
