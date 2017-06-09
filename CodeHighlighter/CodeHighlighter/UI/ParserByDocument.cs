using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CodeHighlighter.UI
{
    public class ParserByDocument : IMessageParser
    {
        public async Task<CodeWithLanguage> Parse(Message message, TelegramBotClient bot)
        {
            if (message.Type != MessageType.DocumentMessage)
                return new CodeWithLanguage();

            var file = await bot.GetFileAsync(message.Document.FileId);

            var filename = message.Document.FileName;

            using (var saveDocumentStream = System.IO.File.Open(filename, FileMode.Create))
            {
                await file.FileStream.CopyToAsync(saveDocumentStream);
            }

            var text = System.IO.File.ReadAllText(filename);
            await bot.SendTextMessageAsync(message.Chat.Id, "Thx for the code");

            return new CodeWithLanguage(text, "python");
        }
    }
}
