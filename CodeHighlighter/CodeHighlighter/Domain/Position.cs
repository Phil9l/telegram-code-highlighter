namespace CodeHighlighter.Domain
{
    public struct Position
    {
        private int lineIndex, charIndex;
        private object p;

        public Position(int lineIndex, object p) : this()
        {
            this.lineIndex = lineIndex;
            this.p = p;
        }
    }
}