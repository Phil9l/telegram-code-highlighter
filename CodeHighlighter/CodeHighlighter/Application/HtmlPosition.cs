namespace CodeHighlighter.Application
{
    internal class HtmlPosition
    {
        public int Row { get; }
        public int Column { get; }

        public HtmlPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
