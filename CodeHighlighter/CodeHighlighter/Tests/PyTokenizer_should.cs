using System.Collections.Generic;
using CodeHighlighter.Application;
using CodeHighlighter.Domain;
using CodeHighlighter.Domain.Tokenizers;
using NUnit.Framework;

namespace CodeHighlighter.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class PyTokenizer_should : TestTokenizer
    {
        private readonly BaseTokenizer tokenizer = new PyTokenizer();

        [Test]
        public void WorkOnBuiltins()
        {
            TestTokens(tokenizer, "help()", new List<ResultToken>
            {
                new ResultToken("help", "builtin"),
                new ResultToken("(", "operator"),
                new ResultToken(")", "operator")
            });
        }

        [Test]
        public void WorkOnComments()
        {
            TestTokens(tokenizer, "# no", new List<ResultToken>
            {
                new ResultToken("# no", "comment")
            });
            TestTokens(tokenizer, "#", new List<ResultToken>
            {
                new ResultToken("#", "comment")
            });
        }

        [Test]
        public void WorkOnIndents()
        {
            TestTokens(tokenizer, "try:\n    pass\nexcept:\n    pass", new List<ResultToken>
            {
                new ResultToken("try", "keyword"),
                new ResultToken(":", "operator"),
                new ResultToken("\n", "linebreak"),
                new ResultToken("    ", "new-indent"),
                new ResultToken("pass", "keyword"),
                new ResultToken("\n", "linebreak"),
                new ResultToken("except", "keyword"),
                new ResultToken(":", "operator"),
                new ResultToken("\n", "linebreak"),
                new ResultToken("    ", "new-indent"),
                new ResultToken("pass", "keyword")
            });
        }

        [Test]
        public void WorkOnKeywords()
        {
            TestTokens(tokenizer, "break", new List<ResultToken>
            {
                new ResultToken("break", "keyword")
            });
            TestTokens(tokenizer, "while True", new List<ResultToken>
            {
                new ResultToken("while", "keyword"),
                new ResultToken(" ", ""),
                new ResultToken("True", "keyword")
            });
        }

        [Test]
        public void WorkOnNumbers()
        {
            TestTokens(tokenizer, "42", new List<ResultToken>
            {
                new ResultToken("42", "number")
            });
            TestTokens(tokenizer, "1 22", new List<ResultToken>
            {
                new ResultToken("1", "number"),
                new ResultToken(" ", ""),
                new ResultToken("22", "number")
            });
        }
    }
}