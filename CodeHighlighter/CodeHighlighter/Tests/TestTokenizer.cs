using System.Collections.Generic;
using System.Linq;
using CodeHighlighter.Application;
using CodeHighlighter.Domain;
using NUnit.Framework;

namespace CodeHighlighter.Tests
{
    [TestFixture]
    public class TestTokenizer
    {
        protected void TestTokens(BaseTokenizer tokenizer, string text, List<ResultToken> expected)
        {
            var highlighter = new Highlighter();
            var tokens = highlighter.TokenizeSourceCode(text, tokenizer);
            Assert.AreEqual(expected, tokens.ToList());
        }
    }
}