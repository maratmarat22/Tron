using System.Net;

namespace Tron.Server.Domain
{
    internal struct Remote
    {
        public EndPoint EndPoint { get; set; }
        
        public int Id { get; private set; }

        internal Remote(EndPoint endPoint, int id)
        {
            EndPoint = endPoint;
            Id = id;
        }
    }
}
