namespace Tron.Common.Persistence
{
    public class FileProcessor
    {
        public static (string, int) ReadSocket(string path)
        {
            string[] socket = File.ReadAllText(path).Split('/');
            string ip = socket[0];
            int port = int.Parse(socket[1]);

            return (ip, port);
        }

        public static string? ReadUsername(string path)
        {
            string? username;
            
            try
            {
                username = File.ReadAllText(path);
            }
            catch (Exception)
            {
                username = null;
            }

            return username;
        }

        public static void WriteUsername(string path, string username)
        {
            File.WriteAllText(path, username);
        }
    }
}
