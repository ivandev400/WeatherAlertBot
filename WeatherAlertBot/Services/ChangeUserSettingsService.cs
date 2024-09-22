using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService : IChangeUserSettingsService
    {
        private Logger<UserExistsService> logger;
        private IUserExistsService userExistsService;

        public ChangeUserSettingsService(IUserExistsService userExistsService, Logger<UserExistsService> logger)
        {
            this.userExistsService = userExistsService;
            this.logger = logger;
        }

        public void ChangeUserSettingsLocation(User user, string location)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.Location = location;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Can't change user location setting, {ex}");
            }
        }
        public void ChangeUserSettingsUpdateInterval(User user, string updateInterval)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.UpdateInterval = updateInterval;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Can't change user location setting, {ex}");
            }
        }
        public void ChangeUserSettingsLocation(User user, TimeOnly morningTime)
        {
            try
            {
                if (userExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.MorningTime = morningTime;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Can't change user location setting, {ex}");
            }
        }
    }
}
