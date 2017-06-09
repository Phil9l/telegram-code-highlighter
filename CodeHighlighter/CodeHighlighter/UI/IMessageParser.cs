using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CodeHighlighter.UI
{
    public interface IMessageParser
    {
        Task<CodeWithLanguage> Parse(Message message, TelegramBotClient bot);
    }
}
