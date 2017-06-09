using System.Collections.Generic;
using System.Linq;
using CodeHighlighter.Domain;
using CodeHighlighter.Domain.Tokenizers;
using NUnit.Framework;

namespace CodeHighlighter.Tests
{
    [TestFixture]
    public class TestTokenizer
    {
        protected void TestTokens(BaseTokenizer tokenizer, string text, List<Token> expected)
        {
            var result = tokenizer.Tokenize(new[] {text}).ToList();
            Assert.AreEqual(result, expected);
        }
    }

    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class PyTokenizer_should : TestTokenizer
    {
        [Test]
        public void WorkOnPyComments()
        {
            var tokenizer = new PyTokenizer();
            TestTokens(tokenizer, "# no", new List<Token>
            {
                new Token(TokenTypes.Comment, "# no",
                    new Position(1, 0), new Position(1, 4))
            });
        }
    }
}