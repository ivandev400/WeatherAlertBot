using WeatherAlertBot.Models;

namespace WeatherAlertBot.Interfaces
{
    public interface IChangeUserSettingsService
    {
        public void ChangeUserSettingsLocation(User user, string location);
        public void ChangeUserSettingsUpdateInterval(User user, string updateInterval);
        public void ChangeUserSettingsMorningTime(User user, TimeOnly morningTime);
    }
}
