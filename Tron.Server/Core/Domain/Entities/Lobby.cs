using System.Net;

namespace Tron.Server.Core.Domain.Entities
{
    internal class Lobby
    {
        internal EndPoint Master { get; }

        internal string Hostname { get; }

        internal bool IsPrivate { get; }

        internal string Password { get; }

        internal Lobby(EndPoint master, string hostname, bool isPrivate, string password)
        {
            Master = master;
            Hostname = hostname;
            IsPrivate = isPrivate;
            Password = password;
        }

        public override string ToString() => $"{Master}/{Hostname}/{IsPrivate}/{Password}";
    }
}
