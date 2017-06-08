using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Examples.Echo
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("Token");
        private static Dictionary<string, bool> cancelled = new Dictionary<string, bool>();

        static void Main(string[] args)
        {
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || (message.Type != MessageType.TextMessage && message.Type != MessageType.DocumentMessage)) return;

            if(message.Type == MessageType.DocumentMessage)
            {
                var username = message.Chat.Username;
                cancelled[username] = false;

                var file = await Bot.GetFileAsync(message.Document.FileId);

                var filename = message.Document.FileName;

                using (var saveDocumentStream = System.IO.File.Open(filename, FileMode.Create))
                {
                    await file.FileStream.CopyToAsync(saveDocumentStream);
                }

                await Bot.SendTextMessageAsync(message.Chat.Id, "Thx for the code");

                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("Python"),
                        new InlineKeyboardButton("C++"),
                        new InlineKeyboardButton("Cancel"),
                    }
                });

                await Bot.SendTextMessageAsync(message.Chat.Id, "Choose your language!",
                     replyMarkup: keyboard);

                await Task.Delay(3000); // Imitation of highlighter work
                if(!cancelled[username])
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id,
                        "Here is your link: http://www.youtube.com!");
                }
                else
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Ok, your task has been canceled");
                }
            }
            else
            {
                var usage = "Upload your code by document to make it look awesome";

                await Bot.SendTextMessageAsync(message.Chat.Id, usage,
                    replyMarkup: new ReplyKeyboardHide());
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {

            var answer = callbackQueryEventArgs.CallbackQuery.Data;
            if (answer == "Cancel")
            {
                var username = callbackQueryEventArgs.CallbackQuery.Message.Chat.Username;
                cancelled[username] = true;
                Console.WriteLine("Canceled task");
            }
            else
            {
                Console.WriteLine("User has chosen: " + answer);
            }
        }
    }
}