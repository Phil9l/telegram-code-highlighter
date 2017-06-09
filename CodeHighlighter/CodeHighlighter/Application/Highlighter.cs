using System.Collections.Generic;
using CodeHighlighter.Domain;
using System;

namespace CodeHighlighter.Application {
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

        public IEnumerable<ResultToken> TokenizeSourceCode(string source, BaseTokenizer tokenizer) {
            string[] lines = source.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var written = new Position(1, 0);
            foreach (var tok in tokenizer.Tokenize(lines)) {
                string tType = tok.Type.ToString();
                string tStr = tok.Content;

                var startPos = new Position(tok.Start.LineIndex, tok.Start.CharIndex);
                var endPos = new Position(tok.End.LineIndex, tok.End.CharIndex);
                var currentToken = new ResultToken(tStr, tType);

                string resultType;
                if (!tokenTransformDict.TryGetValue(tok.Type, out resultType)) {
                    resultType = "";
                }

                if (resultType != "") {
                    if (!written.Equals(startPos)) {
                        text, written = _combine_range(lines, written, start_pos);
                        if (text != "") {
                            yield return new ResultToken(text, "");
                        }
                    }
                    var text = currentToken.Content;
                    written = endPos;
                    if (text != "") {
                        yield return new ResultToken(text, resultType);
                    }
                }
            }
            line_upto_token, written = _combine_range(lines, written, end_pos);
            if (line_upto_token != "") {
                yield return new ResultToken(line_upto_token, '');
            }
        }
    }
}