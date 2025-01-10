using System.Net;

namespace Tron.Common.Entities
{
    public class Lobby
    {
        public string Master { get; }

        public string HostName { get; }

        public bool IsPrivate { get; }

        public string Privacy { get; }

        public string Password { get; }

        public Lobby(string master, string hostname, bool isPrivate, string password)
        {
            Master = master;
            HostName = hostname;
            IsPrivate = isPrivate;
            Privacy = IsPrivate ? "PRIVATE" : "PUBLIC";
            Password = password;
        }
    }
}
