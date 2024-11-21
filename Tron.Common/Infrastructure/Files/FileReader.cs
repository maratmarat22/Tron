namespace Tron.Common.Infrastructure.Files
{
    public class FileReader
    {
        public (string, int) ReadSocket(string filePath)
        {
            string[] socket = File.ReadAllText(filePath).Split('/');
            string ip = socket[0];
            int port = int.Parse(socket[1]);

            return (ip, port);
        }
    }
}
