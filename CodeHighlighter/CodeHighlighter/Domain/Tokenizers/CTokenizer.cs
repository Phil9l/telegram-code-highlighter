using System.Collections.Generic;

namespace CodeHighlighter.Domain.Tokenizers
{
    public class CTokenizer : BaseTokenizer
    {
        public override string Name => "C";

        public override List<string> Extensions => new List<string>
        {
            ".c"
        };

        public override List<TokenMapping> Tokens => new List<TokenMapping>
        {
            new TokenMapping("^    ", TokenTypes.Indent),
            new TokenMapping(@"#include", TokenTypes.Import),
            new TokenMapping(@"<[A-Za-z0-9_.]+>", TokenTypes.Library),
            new TokenMapping(@"//.*$", TokenTypes.Comment),
            new TokenMapping("\"[^\"]*\"", TokenTypes.String),
            new TokenMapping("'[^']*'", TokenTypes.String),
            new TokenMapping(@"0b[01]+", TokenTypes.Number),
            new TokenMapping(@"0x[0-9a-f]", TokenTypes.Number),
            new TokenMapping(@"\d+", TokenTypes.Number),
            new TokenMapping(@"\d+.\d+", TokenTypes.Number),
            new TokenMapping(@"(\+=|-=|/=|\*=|==|<|>|<=|>=|\+|-|\*|/|\*\*|//|:|\[|\]|\(|\)|`|%|%=|!=)", TokenTypes.Op),
            new TokenMapping(@"\d*[A-Za-z_]\w*", TokenTypes.Name),
            new TokenMapping("\n", TokenTypes.LineBreak)
        };

        public override HashSet<string> Builtins => new HashSet<string>
{
            "asm",
            "auto",
            "break",
            "case",
            "const",
            "continue",
            "default",
            "do",
            "else",
            "enum",
            "extern",
            "for",
            "goto",
            "if",
            "register",
            "restricted",
            "return",
            "sizeof",
            "static",
            "struct",
            "switch",
            "typedef",
            "union",
            "volatile",
            "while",
            "bool",
            "int",
            "long",
            "float",
            "short",
            "double",
            "char",
            "unsigned",
            "signed",
            "void",
        };

        public override HashSet<string> Keywords => new HashSet<string>
        {
            "true",
            "false",
            "NULL",
        };

        public override string Variable => "";
        public override bool CheckVariable => false;
        public override string CorrectName => @"^[A-Za-z_]\w*$";
    }
}