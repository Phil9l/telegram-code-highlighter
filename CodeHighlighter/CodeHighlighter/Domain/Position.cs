namespace CodeHighlighter.Domain
{
    public struct Position
    {
        public int LineIndex { get; set; }

        public int CharIndex { get; set; }

        public Position(int lineIndex, int charIndex)
        {
            LineIndex = lineIndex;
            CharIndex = charIndex;
        }
    }
}