using System.Collections.Generic;
using CodeHighlighter.Domain;
using System;
using System.Net;
using System.Text;
using System.IO;

namespace CodeHighlighter.Application
{
    internal class Highlighter
    {
        private readonly Dictionary<TokenTypes, string> tokenTransformDict = new Dictionary<TokenTypes, string>
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

        private Tuple<string, Position> CombineRange(IReadOnlyList<string> lines, Position start, Position end)
        {
            if (start.LineIndex == end.LineIndex)
                return new Tuple<string, Position>
                    (lines[start.LineIndex - 1].Substring(start.CharIndex, end.CharIndex - start.CharIndex), end);
            var sb = new StringBuilder(lines[start.LineIndex - 1].Substring(start.CharIndex));
            for (var i = start.LineIndex; i < end.LineIndex - 1; i++)
            {
                sb.Append(lines[i]);
            }
            sb.Append(lines[end.LineIndex - 1].Substring(0, end.CharIndex));
            return new Tuple<string, Position>
                (sb.ToString(), end);
        }


        public IEnumerable<ResultToken> TokenizeSourceCode(string source, BaseTokenizer tokenizer)
        {

            string[] lines = source.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var written = new Position(1, 0);
            Position endPos = new Position(0, 0);
            foreach (var tok in tokenizer.Tokenize(lines))
            {
                string tType = tok.Type.ToString();
                string tStr = tok.Content;

                var startPos = new Position(tok.Start.LineIndex, tok.Start.CharIndex);
                endPos = new Position(tok.End.LineIndex, tok.End.CharIndex);
                var currentToken = new ResultToken(tStr, tType);

                string resultType;
                if (!tokenTransformDict.TryGetValue(tok.Type, out resultType))
                {
                    resultType = "";
                }

                if (resultType != "")
                {
                    if (!written.Equals(startPos))
                    {
                        var result = CombineRange(lines, written, startPos);
                        var resultText = result.Item1;
                        written = result.Item2;
                        if (resultText != "")
                        {
                            yield return new ResultToken(resultText, "");
                        }
                    }
                    var text = currentToken.Content;
                    written = endPos;
                    if (text != "")
                    {
                        yield return new ResultToken(text, resultType);
                    }
                }
            }
            var secondResult = CombineRange(lines, written, endPos);
            var lineUptoToken = secondResult.Item1;

            if (lineUptoToken != "")
            {
                yield return new ResultToken(lineUptoToken, "");
            }
        }

        public string HtmlHighlight(IEnumerable<ResultToken> classifiedText)
        {
            var result = new List<string>();
            foreach (var token in classifiedText)
            {
                if (token.Type == "linebreak")
                {
                    result.Add("<br />");
                    continue;
                }

                if (token.Type == "dedent")
                {
                    result.Add("</div></div>");
                }
                if (token.Type != "")
                {
                    result.Add($"<span class=\"{token.Type}\">");
                }

                result.Add(WebUtility.HtmlEncode(token.Content));
                if (token.Type != "")
                {
                    result.Add("</span>");
                }
                if (token.Type == "new-indent")
                {
                    result.Add("<div class=\"content-block\"><span class=\"toggle\">-</span><div class=\"line-container\"><div class=\"line\"></div><br /></div><div class=\"content\">");
                }
            }
            return String.Join("", result);
        }

        public string GetHTMLPage(string template, string title, string className, string content)
        {
            return String.Format(template, title, className, content);
        }

        public string GetHTMLPage(string title, string className, string content)
        {
            string template = File.ReadAllText("template.html");
            return GetHTMLPage(template, title, className, content);
        }
    }
}