using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService : IChangeUserSettingsService
    {
        private Logger<UserExistsService> _logger;
        private IUserExistsService _userExistsService;

        public ChangeUserSettingsService(IUserExistsService userExistsService)
        {
            _userExistsService = userExistsService;
        }

        public void ChangeUserSettingsLocation(User user, string location)
        {
            try
            {
                if (_userExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.Location = location;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
        }
        public void ChangeUserSettingsUpdateInterval(User user, string updateInterval)
        {
            try
            {
                if (_userExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.UpdateInterval = updateInterval;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
        }
        public void ChangeUserSettingsLocation(User user, TimeOnly morningTime)
        {
            try
            {
                if (_userExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.MorningTime = morningTime;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
        }
    }
}
