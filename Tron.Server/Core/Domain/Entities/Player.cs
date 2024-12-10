using System.Net;
using Tron.Common.Messages.General;

namespace Tron.Server.Core.Domain.Entities
{
    internal class Player
    {
        internal int Id { get; set; }
        
        internal IPEndPoint Point { get; private set; }

        internal string Name { get; private set; }

        internal Color Color { get; private set; }

        internal int Points { get; private set; }

        internal Player(int id, IPEndPoint point, string name, Color color)
        {
            Id = id;
            Point = point;
            Name = name;
            Color = color;
            Points = 0;
        }
    }
}
