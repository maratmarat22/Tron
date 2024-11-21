namespace Tron.Common.Domain
{
    public struct Message
    {
        public Headers Header { get; private set; }
        
        public List<string> Segments { get; private set; }

        public Message(string message)
        {
            var segments = message.Split('/');
            
            Header = (Headers)int.Parse(segments[0]);
            Segments = [.. segments[1..]];
        }
    }
}
