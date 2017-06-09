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
            var container = new StandardKernel();

            container.Bind(kernel => kernel
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<IMessageParser>()
                .BindAllInterfaces());
            var bot = container.Get<TelegramBot>();
            bot.Serve();
        }
    }
}