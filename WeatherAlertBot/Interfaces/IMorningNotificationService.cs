using Telegram.Bot.Types;
using User = WeatherAlertBot.Models.User;

namespace WeatherAlertBot.Interfaces
{
    public interface IMorningNotificationService
    {
        public Task GetMorningNotification(User user, Update update);
    }
}
