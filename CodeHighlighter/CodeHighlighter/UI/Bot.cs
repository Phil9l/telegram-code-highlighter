using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CodeHighlighter.Application;
using CodeHighlighter.Domain;
using CodeHighlighter.Domain.Tokenizers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace CodeHighlighter.UI
{
    internal class TelegramBot
    {
        private readonly TelegramBotClient bot = new TelegramBotClient("393926966:AAG158H_fhtctWo97uTB8R0ZQIgQDdq02Zc");
        private readonly Dictionary<string, bool> cancelled = new Dictionary<string, bool>();

        public TelegramBot(IMessageParser[] parsers, BaseTokenizer[] tokenizers)
        {
            Parsers = parsers;
            Tokenizers = tokenizers;
        }

        public IMessageParser[] Parsers { get; set; }
        public BaseTokenizer[] Tokenizers { get; }

        public void Serve()
        {
            bot.OnMessage += BotOnMessageReceived;
            bot.OnMessageEdited += BotOnMessageReceived;
            bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            bot.StartReceiving();
            Console.WriteLine("I am alive!");
            Console.ReadLine();
            bot.StopReceiving();
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage &&
                message.Type != MessageType.DocumentMessage) return;

            if (message.Type == MessageType.TextMessage)
                return; // Not supported

            var username = message.Chat.Username;
            cancelled[username] = false;

            var codeAndLanguage = await GetCodeFromMessage(message);
            var highlighter = new Highlighter();
            var text = codeAndLanguage.Code;
            var language = codeAndLanguage.Language;
            text = text.Replace("\r\n", "\n"); // For windows newlines
            var tokenizer = GetTokenizer(language);
            var tokens = highlighter.TokenizeSourceCode(text, tokenizer); // Change to selection
            var result = highlighter.HtmlHighlight(tokens);
            var page = highlighter.GetHTMLPage(language, language, result);
            var path = "Z:\\home\\localhost\\www\\"; // Path to web folder
            var randomName = Guid.NewGuid().ToString();
            File.WriteAllText(path + randomName + ".html", page);

            if (!cancelled[username])
                await bot.SendTextMessageAsync(message.Chat.Id,
                    "Here is your link: http://127.0.0.1/" + randomName + ".html");
            else
            {
                const string usage = "Upload your code by document to make it look awesome";

                await bot.SendTextMessageAsync(message.Chat.Id, usage,
                    replyMarkup: new ReplyKeyboardHide());
            }
        }

        private BaseTokenizer GetTokenizer(string language)
        {
            foreach (var tokenizer in Tokenizers)
            {
                if (tokenizer.Name == language)
                    return tokenizer;
            }
            return new BashTokenizer();
        }

        private async Task<CodeWithLanguage> GetCodeFromMessage(Message message)
        {
            foreach (var messageParser in Parsers)
            {
                var result = await messageParser.Parse(message, bot);
                if (result.Code != "")
                    return result;
            }
            return new CodeWithLanguage("", "");
        }

        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var answer = callbackQueryEventArgs.CallbackQuery.Data;
            if (answer == "Cancel")
            {
                var username = callbackQueryEventArgs.CallbackQuery.Message.Chat.Username;
                if (!cancelled[username])
                {
                    cancelled[username] = true;
                    Console.WriteLine("Canceled task");
                    await bot.SendTextMessageAsync(callbackQueryEventArgs.CallbackQuery.Message.Chat.Id,
                        "Ok, your task has been canceled");
                }
            }
            else
            {
                Console.WriteLine("User has chosen: " + answer);
            }
        }
    }
}