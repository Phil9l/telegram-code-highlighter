namespace CodeHighlighter.Domain
{
    public enum TokenTypes
    {
        Comment,
        Number,
        Op,
        String,
        Indent,
        Dedent,
        Name,
        Builtin,
        Keyword,
        LineBreak,
        NewIndent,
        IncorrectName,
        Variable,
        Import,
        Library,
        MagicMethod,
        Decorator
    }
}