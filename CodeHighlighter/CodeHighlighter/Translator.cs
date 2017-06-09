using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeHighlighter
{
    public enum TokenTypes
    {
        COMMENT,
        NUMBER,
        OP,
        STRING,
        INDENT,
        DEDENT,
        NAME,
        BUILTIN,
        KEYWORD,
        LINE_BREAK,
        NEW_INDENT,
        INCORRECT_NAME,
        VARIABLE,
        IMPORT,
        LIBRARY,
        MAGIC_METHOD,
        DECORATOR
    }

    public class Token
    {
        public readonly string Content;
        public readonly Position End;
        public readonly Position Start;

        public Token(TokenTypes type, string content, Position start, Position end)
        {
            Type = type;
            Content = content;
            Start = start;
            End = end;
        }

        public TokenTypes Type { get; set; }
    }

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

    public struct Position
    {
        private int LineIndex, CharIndex;
        private object p;

        public Position(int lineIndex, object p) : this()
        {
            LineIndex = lineIndex;
            this.p = p;
        }
    }

    public abstract class BaseTokenizer
    {
        public abstract string Name { get; }
        public abstract List<string> Extensions { get; }
        public abstract List<TokenMapping> Tokens { get; }
        public abstract HashSet<string> Builtins { get; }
        public abstract HashSet<string> Keywords { get; }
        public abstract string Variable { get; }
        public abstract bool CheckVariable { get; }
        public abstract string CorrectName { get; }

        private bool CheckBuiltin(string token)
        {
            return Builtins.Contains(token);
        }

        private bool CheckKeyword(string token)
        {
            return Keywords.Contains(token);
        }

        private ParsedData ParseLine(string line)
        {
            var result = new ParsedData();
            result.Found = false;
            var nearestIndex = -1;
            foreach (var tokenMapping in Tokens)
            {
                var v = Regex.Match(line, tokenMapping.RegularExpression);
                if (nearestIndex == -1 || v.Index < nearestIndex)
                    result = new ParsedData(v, tokenMapping.Type);
            }
            return result;
        }

        public IEnumerable<Token> HandleLine(string line, int lineIndex)
        {
            var offset = 0;
            while (line.Length > 0)
            {
                var buf = ParseLine(line);
                var found = buf.Found;
                var regexToken = buf.RegularExpression;
                var tokenType = buf.Type;

                if (tokenType == TokenTypes.NAME && CheckBuiltin(regexToken.Groups[0].Value))
                    tokenType = TokenTypes.BUILTIN;
                if (tokenType == TokenTypes.NAME && CheckKeyword(regexToken.Groups[0].Value))
                    tokenType = TokenTypes.KEYWORD;
                if (!found)
                    break;
                var start = new Position(lineIndex, offset + regexToken.Index);
                var end = new Position(lineIndex, offset + regexToken.Index + regexToken.Length);

                var content = regexToken.Groups[0].Value;
                if (tokenType == TokenTypes.NAME)
                {
                    if (!Regex.Match(CorrectName, content).Success)
                        tokenType = TokenTypes.INCORRECT_NAME;
                    if (CheckVariable && Regex.Match(Variable, content).Success)
                        tokenType = TokenTypes.VARIABLE;
                }
                yield return new Token(tokenType, content, start, end);
                var regexEnd = regexToken.Index + regexToken.Length;
                offset += regexEnd;
                line = line.Substring(regexEnd);
            }
        }

        public IEnumerable<Token> Tokenize(IEnumerable<string> lines)
        {
            var indent = 0;
            var currentIndent = 0;
            var index = 0;

            foreach (var line in lines)
            {
                index++;
                foreach (var token in HandleLine(line, index))
                {
                    if (token.Type == TokenTypes.INDENT)
                    {
                        currentIndent++;
                        if (currentIndent > indent)
                        {
                            indent = currentIndent;
                            token.Type = TokenTypes.NEW_INDENT;
                        }
                    }
                    else if (token.Type == TokenTypes.LINE_BREAK)
                    {
                        currentIndent = 0;
                    }
                    else
                    {
                        while (indent > currentIndent)
                        {
                            indent--;
                            yield return new Token(TokenTypes.DEDENT, "", token.Start, token.Start);
                        }
                    }
                    yield return token;
                }
            }
        }
    }
}