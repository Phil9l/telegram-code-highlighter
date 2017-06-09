using System.Collections.Generic;

namespace CodeHighlighter
{
    internal class Highlighter
    {
        private Dictionary<TokenTypes, string> tokenTransformDict = new Dictionary<TokenTypes, string>
        {
            {TokenTypes.Comment, "comment"},
            {
                TokenTypes.Number, "number"
            },
            {
                TokenTypes.Op, "operator"
            },
            {
                TokenTypes.String, "string"
            },
            {
                TokenTypes.Builtin, "builtin"
            },
            {
                TokenTypes.Keyword, "keyword"
            },
            {
                TokenTypes.Indent, "indent"
            },
            {
                TokenTypes.Dedent, "dedent"
            },
            {
                TokenTypes.NewIndent, "new-indent"
            },
            {
                TokenTypes.IncorrectName, "incorrect-name"
            },
            {
                TokenTypes.LineBreak, "linebreak"
            },
            {
                TokenTypes.Variable, "variable"
            },
            {
                TokenTypes.Import, "import"
            },
            {
                TokenTypes.Library, "library"
            },
            {
                TokenTypes.MagicMethod, "magic-method"
            },
            {
                TokenTypes.Decorator, "decorator"
            }
        };
    }
}