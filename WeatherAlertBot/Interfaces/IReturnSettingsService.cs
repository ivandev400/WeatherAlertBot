using Telegram.Bot.Types;

namespace WeatherAlertBot.Interfaces
{
    public interface IReturnSettingsService
    {
        public string ReturnSettings(Update update);
    }
}
