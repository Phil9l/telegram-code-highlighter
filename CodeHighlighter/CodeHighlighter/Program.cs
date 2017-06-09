using System.IO;
using CodeHighlighter.Application;
using CodeHighlighter.Domain.Tokenizers;
using CodeHighlighter.UI;
using Ninject;
using Ninject.Extensions.Conventions;

namespace CodeHighlighter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var text = "def f(x):\n\treturn x + 1";
            //var highlighter = new Highlighter();
            //var tokens = highlighter.TokenizeSourceCode(text, new PyTokenizer());
            //var result = highlighter.HtmlHighlight(tokens);
            //var page = highlighter.GetHTMLPage("python", "python", result);
            //File.WriteAllText("test.html", page);

            var container = new StandardKernel();

            //container.Bind(kernel => kernel
            //    .FromThisAssembly()
            //    .SelectAllClasses()
            //    .InheritedFrom<IC>()
            //    .BindAllInterfaces());
            var bot = container.Get<TelegramBot>();
            bot.Serve();
        }
    }
}