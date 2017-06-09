namespace CodeHighlighter.Domain
{
    public struct TokenMapping
    {
        public string RegularExpression;
        public TokenTypes Type;

        public TokenMapping(string regularExpression, TokenTypes type)
        {
            RegularExpression = regularExpression;
            Type = type;
        }
    }
}