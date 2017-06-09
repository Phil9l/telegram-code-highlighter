using System.Text.RegularExpressions;

namespace CodeHighlighter.Domain
{
    public struct ParsedData
    {
        public Match RegularExpression;
        public TokenTypes Type;
        public bool Found;

        public ParsedData(Match regularExpression, TokenTypes type, bool found)
        {
            RegularExpression = regularExpression;
            Type = type;
            Found = found;
        }

        public ParsedData(Match regularExpression, TokenTypes type) : this(regularExpression, type, true)
        {
        }
    }
}