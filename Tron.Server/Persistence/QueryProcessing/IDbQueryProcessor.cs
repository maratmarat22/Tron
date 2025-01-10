namespace Tron.Server.Persistence.QueryProcessing
{
    internal interface IDbQueryProcessor
    {
        internal bool TryRegister(string username);

        internal bool TryLogIn(string username);

        internal string[] ReadTopTen();

        internal void UpdatePlayer();
    }
}
