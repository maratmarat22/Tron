using System.Text;

namespace Tron.Common.Messages
{
    public class ReturnLobbiesMessage : Message
    {
        public List<string> Lobbies { get; }

        public ReturnLobbiesMessage(Header header, List<string> segments) : base(header, segments)
        {
            Lobbies = segments;
        }

        public ReturnLobbiesMessage(List<string> lobbies)
        {
            Header = Header.RETURN_LOBBIES;
            Lobbies = lobbies;
        }

        public override string ToString()
        {
            StringBuilder message = new();
            
            message.Append(Header.ToString());

            foreach (var lobby in Lobbies)
            {
                message.Append($"/{lobby.ToString()}");
            }

            return message.ToString();
        }
    }
}
