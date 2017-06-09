using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        private readonly TelegramBotClient bot = new TelegramBotClient("Token");
        private readonly Dictionary<string, bool> cancelled = new Dictionary<string, bool>();

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

            if (message.Type == MessageType.DocumentMessage)
            {
                var username = message.Chat.Username;
                cancelled[username] = false;

                var file = await bot.GetFileAsync(message.Document.FileId);

                var filename = message.Document.FileName;

                using (var saveDocumentStream = File.Open(filename, FileMode.Create))
                {
                    await file.FileStream.CopyToAsync(saveDocumentStream);
                }

                await bot.SendTextMessageAsync(message.Chat.Id, "Thx for the code");

                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("Python"),
                        new InlineKeyboardButton("C++"),
                        new InlineKeyboardButton("Cancel")
                    }
                });

                await bot.SendTextMessageAsync(message.Chat.Id, "Choose your language!",
                    replyMarkup: keyboard);


                await Task.Delay(5000); // Imitation of highlighter work

                if (!cancelled[username])
                    await bot.SendTextMessageAsync(message.Chat.Id,
                        "Here is your link: http://www.youtube.com!");
            }
            else
            {
                var usage = "Upload your code by document to make it look awesome";

                await bot.SendTextMessageAsync(message.Chat.Id, usage,
                    replyMarkup: new ReplyKeyboardHide());
            }
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