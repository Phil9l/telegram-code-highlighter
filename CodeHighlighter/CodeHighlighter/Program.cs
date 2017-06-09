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
            //var tokenizer = new PyTokenizer();
            //var tokens = tokenizer.Tokenize(new List<string> { "def f(x):", "   return x + 5" });
            //foreach (var token in tokens)
            //{
            //    Console.WriteLine(token.Content);
            //    Console.WriteLine(token.Type);
            //}


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