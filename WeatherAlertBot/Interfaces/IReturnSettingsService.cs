using Telegram.Bot.Types;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Interfaces
{
    public interface IReturnSettingsService
    {
        public string ReturnSettingsToString(Update update);
        public UserSettings ReturnSettings(Update update);
    }
}
