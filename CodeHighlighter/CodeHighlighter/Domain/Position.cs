namespace CodeHighlighter.Domain
{
    public struct Position
    {
        private int lineIndex, charIndex;
        public int LineIndex {
            get {
                return lineIndex;
            }
            set {
                lineIndex = value;
            }
        }
        public int CharIndex {
            get {
                return charIndex;
            }
            set {
                charIndex = value;
            }
        }
        private object p;

        public Position(int lineIndex, object p) : this()
        {
            this.lineIndex = lineIndex;
            this.p = p;
        }
    }
}