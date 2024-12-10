namespace Tron.Server.Core.Domain.Entities
{
    internal class GameState
    {
        public Lobby Lobby { get; private set; }

        public Player Winner { get; private set; }
    }
}
