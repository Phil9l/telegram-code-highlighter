using System.Collections.Generic;

namespace CodeHighlighter
{
    internal class BashTokenizer : BaseTokenizer
    {
        public override string Name => "Bash";

        public override List<string> Extensions => new List<string>
        {
            ".sh"
        };

        public override List<TokenMapping> Tokens => new List<TokenMapping>
        {
            new TokenMapping("^    ", TokenTypes.INDENT),
            new TokenMapping(@"#!.*$", TokenTypes.COMMENT),
            new TokenMapping("\"[^\"]*\"", TokenTypes.STRING),
            new TokenMapping("'[^']*'", TokenTypes.STRING),
            new TokenMapping(@"\d+", TokenTypes.NUMBER),
            new TokenMapping(@"(\+=|-=|/=|\*=|==|<|>|<=|>=|\+|-|\*|/|\*\*|//|:|\[|\]|\(|\)|`|%|%=|!=)", TokenTypes.OP),
            new TokenMapping(@"\d*[A-Za-z_$][A-Za-z0-9_$\{}#]*", TokenTypes.NAME),
            new TokenMapping("\n", TokenTypes.LINE_BREAK)
        };

        public override HashSet<string> Builtins => new HashSet<string>
        {
            "alias",
            "bg",
            "bind",
            "break",
            "builtin",
            "caller",
            "cd",
            "command",
            "compgen",
            "complete",
            "declare",
            "dirs",
            "disown",
            "echo",
            "enable",
            "eval",
            "exec",
            "exit",
            "export",
            "false",
            "fc",
            "fg",
            "getopts",
            "hash",
            "help",
            "history",
            "jobs",
            "kill",
            "let",
            "local",
            "logout",
            "popd",
            "printf",
            "pushd",
            "pwd",
            "read",
            "readonly",
            "set",
            "shift",
            "shopt",
            "source",
            "suspend",
            "test",
            "time",
            "times",
            "trap",
            "true",
            "type",
            "typeset",
            "ulimit",
            "umask",
            "unalias",
            "unset",
            "wait"
        };

        public override HashSet<string> Keywords => new HashSet<string>
        {
            "if",
            "fi",
            "else",
            "while",
            "do",
            "done",
            "for",
            "then",
            "return",
            "function",
            "case",
            "select",
            "continue",
            "until",
            "esac",
            "elif"
        };

        public override string Variable => "";
        public override bool CheckVariable => false;
        public override string CorrectName => @"^[A-Za-z_$#]\w*$";
    }
}