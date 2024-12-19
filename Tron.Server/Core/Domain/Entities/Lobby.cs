namespace Tron.Server.Core.Domain.Entities
{
    internal class Lobby
    {
        internal int Id { get; private set; }

        internal int MaxPlayers { get; private set; }

        internal bool IsPrivate { get; private set; }

        internal string Password { get; private set; }

        internal Player Host { get; private set; }

        internal List<Player> Players { get; private set; }

        internal Lobby(int id, int maxPlayers, bool isPrivate, string password, Player host, params Player[] players)
        {
            Id = id;
            MaxPlayers = maxPlayers;
            IsPrivate = isPrivate;
            Password = password;
            Host = host;
            Players = [.. players];
        }

        internal void Add(Player player)
        {
            if (Players.Count + 1 <= MaxPlayers)
            {
                Players.Add(player);
            }
        }

        internal void Remove(Player player)
        {
            Players.Remove(player);
        }
    }
}
