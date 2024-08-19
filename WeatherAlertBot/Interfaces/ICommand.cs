using Telegram.Bot.Types;
using Telegram.Bot;

namespace WeatherAlertBot.Interfaces
{
    public interface ICommand
    {
        public TelegramBotClient Client { get; }
        public string CommandName { get; }
        public string CommandDescription { get; }
        public Task Execute(Update update);
    }
}
