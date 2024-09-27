using WeatherAlertBot.Models;

namespace WeatherAlertBot.Interfaces
{
    public interface IMorningNotificationService
    {
        public Task SendMorningNotification(User user);
    }
}
