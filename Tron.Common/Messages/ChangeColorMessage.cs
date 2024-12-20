﻿namespace Tron.Common.Messages
{
    public class ChangeColorMessage : Message
    {
        public Color Color { get; }

        public ChangeColorMessage(Header header, List<string> segments) : base(header, segments)
        {
            Color = (Color)int.Parse(segments[0]);
        }

        public ChangeColorMessage(Header header, Color color)
        {
            Header = Header.CHANGE_COLOR;
            Color = color;
        }

        public override string ToString() => ((int)Header).ToString() + '/' + ((int)Color).ToString();
    }
}
