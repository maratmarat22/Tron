namespace Tron.Common.Config.Utilities
{
    public class SocketReader
    {
        public (string, int) Read(string path)
        {
            string[] socket = File.ReadAllText(path).Split('/');
            string ip = socket[0];
            int port = int.Parse(socket[1]);

            return (ip, port);
        }
    }
}
