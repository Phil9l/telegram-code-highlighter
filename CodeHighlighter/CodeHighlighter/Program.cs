﻿using System;
using System.Collections.Generic;
using CodeHighlighter.Tokenizers;
using Ninject;

namespace CodeHighlighter
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenizer = new PyTokenizer();
            //var tokens = tokenizer.Tokenize(new List<string> { "def f(x):", "   return x + 5" });
            //foreach (var token in tokens)
            //{
            //    Console.WriteLine(token.Content);
            //    Console.WriteLine(token.Type);
            //}


            //var container = new StandardKernel();

            //container.Bind(kernel => kernel
            //    .FromThisAssembly()
            //    .SelectAllClasses()
            //    .InheritedFrom<IC>()
            //    .BindAllInterfaces());
            //var bot = container.Get<TelegramBot>();
            //bot.Serve();
            //TelegramBot.Serve();
        }
    }
}