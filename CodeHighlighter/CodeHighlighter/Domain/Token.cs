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

        protected bool Equals(Token other)
        {
            return string.Equals(Content, other.Content) && End.Equals(other.End)
                   && Start.Equals(other.Start) && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Token) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Content != null ? Content.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ End.GetHashCode();
                hashCode = (hashCode * 397) ^ Start.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Type;
                return hashCode;
            }
        }
    }
}