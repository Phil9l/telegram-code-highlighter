using CodeHighlighter.Domain;
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
            container.Bind(kernel => kernel
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<BaseTokenizer>()
                .BindAllBaseClasses());
            var bot = container.Get<TelegramBot>();
            bot.Serve();
        }
    }
}