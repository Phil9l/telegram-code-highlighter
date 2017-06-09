using System.Collections.Generic;

namespace CodeHighlighter.Domain.Tokenizers
{
    internal class PyTokenizer : BaseTokenizer
    {
        public override string Name => "Python";

        public override List<string> Extensions => new List<string>
        {
            ".py"
        };

        public override List<TokenMapping> Tokens => new List<TokenMapping>
        {
            new TokenMapping("^    ", TokenTypes.Indent),
            new TokenMapping(@"#.*$", TokenTypes.Comment),
            new TokenMapping(@"__\w+__", TokenTypes.MagicMethod),
            new TokenMapping(@"@\w+", TokenTypes.Decorator),
            new TokenMapping("\"[^\"]*\"", TokenTypes.String),
            new TokenMapping("'[^']*'", TokenTypes.String),
            new TokenMapping(@"\d+", TokenTypes.Number),
            new TokenMapping(@"(\+=|-=|/=|\*=|==|<|>|<=|>=|\+|-|\*|/|\*\*|//|:|\[|\]|\(|\)|`|%|%=|!=)", TokenTypes.Op),
            new TokenMapping(@"\d*[A-Za-z_]\w*", TokenTypes.Name),
            new TokenMapping("\n", TokenTypes.LineBreak)
        };

        public override HashSet<string> Builtins => new HashSet<string>
        {
            "abs",
            "dict",
            "help",
            "min",
            "setattr",
            "all",
            "dir",
            "hex",
            "next",
            "slice",
            "any",
            "divmod",
            "id",
            "object",
            "sorted",
            "ascii",
            "enumerate",
            "input",
            "oct",
            "staticmethod",
            "bin",
            "eval",
            "int",
            "open",
            "str",
            "bool",
            "exec",
            "isinstance",
            "ord",
            "sum",
            "bytearray",
            "filter",
            "issubclass",
            "pow",
            "super",
            "bytes",
            "float",
            "iter",
            "print",
            "tuple",
            "callable",
            "format",
            "len",
            "property",
            "type",
            "chr",
            "frozenset",
            "list",
            "range",
            "vars",
            "classmethod",
            "getattr",
            "locals",
            "repr",
            "zip",
            "compile",
            "globals",
            "map",
            "reversed",
            "__import__",
            "complex",
            "hasattr",
            "max",
            "round",
            "delattr",
            "hash",
            "memoryview",
            "set"
        };

        public override HashSet<string> Keywords => new HashSet<string>
        {
            "and",
            "as",
            "assert",
            "break",
            "class",
            "continue",
            "def",
            "del",
            "elif",
            "else",
            "except",
            "exec",
            "finally",
            "for",
            "from",
            "global",
            "if",
            "import",
            "in",
            "is",
            "lambda",
            "not",
            "or",
            "pass",
            "print",
            "raise",
            "return",
            "try",
            "while",
            "with",
            "yield",
            "True",
            "False"
        };

        public override string Variable => "";
        public override bool CheckVariable => false;
        public override string CorrectName => @"^[A-Za-z_]\w*$";
    }
}