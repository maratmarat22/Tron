using System.Net;
using Tron.Common.Entities;

namespace Tron.Server.Core.Domain.Entities
{
    internal class Player
    {
        internal int Id { get; private set; }
        
        internal EndPoint Point { get; private set; }

        internal string Name { get; private set; }

        internal Color Color { get; private set; }

        internal int Score { get; private set; }

        internal Player(int id, EndPoint point, string name, Color color, int score)
        {
            Id = id;
            Point = point;
            Name = name;
            Color = color;
            Score = score;
        }
    }
}
