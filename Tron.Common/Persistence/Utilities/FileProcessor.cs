namespace Tron.Common.Config.Utilities
{
    public class FileProcessor
    {
        public (string, int) ReadSocket(string path)
        {
            string[] socket = File.ReadAllText(path).Split('/');
            string ip = socket[0];
            int port = int.Parse(socket[1]);

            return (ip, port);
        }

        public string? TryReadUsername(string path)
        {
            string? username;
            try
            {
                username = File.ReadAllText(path);
            }
            catch (Exception)
            {
                return null;
            }
            return username;
        }

        public void SaveUsername(string path, string username)
        {
            File.WriteAllText(path, username);
        }
    }
}
