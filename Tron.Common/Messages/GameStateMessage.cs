using System.Text;
using Tron.Common.Entities;

namespace Tron.Common.Messages
{
    public class GameStateMessage : Message
    {
        public Dictionary<Color, PlayerCoordinates> GameState { get; set; }

        public GameStateMessage(Header header, List<string> segments) : base(header, segments)
        {
            GameState = [];

            foreach (string segment in segments)
            {
                // "(color)1:(x)27:(y)10"
                string[] data = segment.Split(':');

                Color color = (Color)int.Parse(data[0]);

                int row = int.Parse(data[1]);
                int column = int.Parse(data[2]);
                PlayerCoordinates coordinates = new(row, column);

                GameState.Add(color, coordinates);
            }
        }

        public GameStateMessage(Dictionary<Color, PlayerCoordinates> gameState)
        {
            Header = Header.GAME_STATE;
            GameState = gameState;
        }

        public override string ToString()
        {
            StringBuilder message = new();

            message.Append(Header.ToString());

            foreach (var state in GameState)
            {
                message.Append($"/{state.ToString()}");
            }

            return message.ToString();
        }
    }
}
