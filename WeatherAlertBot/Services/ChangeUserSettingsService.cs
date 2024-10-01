using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService : IChangeUserSettingsService
    {
        private IUserExistsService userExistsService;
        private WeatherService weatherService => new WeatherService();
        private string geocodingApiKey => Bot.GeocodingApiKey;
        private UserContext userContext;

        public ChangeUserSettingsService(IUserExistsService userExistsService, UserContext userContext)
        {
            this.userExistsService = userExistsService;
            this.userContext = userContext;
        }

        public void ChangeUserSettingsLocation(User user, string location)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    var settings = userContext.UserSettings.First(u => u.UserId == user.Id);

                    settings.Location = location;

                    var weatherResult = weatherService.GetCurrentWeatherDataResponse(settings, geocodingApiKey);
                    settings.TimeZone = weatherResult.Result.TimeZone.ToString();
                    userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void ChangeUserSettingsUpdateInterval(User user, string updateInterval)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    var settings = userContext.UserSettings.First(u => u.UserId == user.Id);
                    settings.UpdateInterval = updateInterval;
                    userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void ChangeUserSettingsMorningTime(User user, TimeOnly morningTime)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    var settings = userContext.UserSettings.First(u => u.UserId == user.Id);
                    settings.MorningTime = morningTime;
                    userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return; 
            }
        }

        public void ChangeLanguage(User user, string language)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    var settings = userContext.UserSettings.First(u => u.UserId == user.Id);
                    settings.Language = language;
                    user.Language = language;
                    userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
