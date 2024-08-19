using Telegram.Bot.Types;

namespace WeatherAlertBot.Interfaces
{
    public interface ITelegramUpdateListener
    {
        Task GetUpdate(Update update);
    }
}
