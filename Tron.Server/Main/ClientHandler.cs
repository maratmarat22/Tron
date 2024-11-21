namespace Tron.Server.Main
{
    internal class ClientHandler
    {
        internal ClientHandler()
        {

        }

        internal void Handle()
        {
            PreGameService();

            PlayerAwaitService();

            GameplayService();
        }
    }
}
