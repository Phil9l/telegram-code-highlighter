namespace CodeHighlighter.Domain
{
    public class Token
    {
        public readonly string Content;
        public readonly Position End;
        public readonly Position Start;

        public Token(TokenTypes type, string content, Position start, Position end)
        {
            Type = type;
            Content = content;
            Start = start;
            End = end;
        }

        public TokenTypes Type { get; set; }
    }
}