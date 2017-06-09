using System;
using System.Collections.Generic;
using System.Text;

namespace CodeHighlighter.Application
{
    class HtmlRenderer
    {
        private Tuple<string, HtmlPosition> CombineRange(List<string> lines, HtmlPosition start, HtmlPosition end)
        {
            if (start.Row == end.Row)
                return new Tuple<string, HtmlPosition>
                    (lines[start.Row - 1].Substring(start.Column, end.Column - start.Column), end);
            var sb = new StringBuilder(lines[start.Row - 1].Substring(start.Column));
            for (int i = start.Row; i < end.Row - 1; i++)
            {
                sb.Append(lines[i]);
            }
            sb.Append(lines[end.Row - 1].Substring(0, end.Column));
            return new Tuple<string, HtmlPosition>
                (sb.ToString(), end);
        }
    }
}
