using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService
    {
        private Logger<IfUserExistsService> _logger;
        private IfUserExistsService _ifUserExistsService;
        
        public ChangeUserSettingsService(Logger<IfUserExistsService> logger, IfUserExistsService ifUserExistsService)
        {
            _logger = logger;
            _ifUserExistsService = ifUserExistsService;
        }

        public void ChangeUserSettingsLocation(Models.User user, string location)
        {
            try
            {
                if (_ifUserExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.Location = location;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
        }
        public void ChangeUserSettingsUpdateInterval(Models.User user, string updateInterval)
        {
            try
            {
                if (_ifUserExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.UpdateInterval = updateInterval;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
        }
        public void ChangeUserSettingsLocation(Models.User user, TimeOnly morningTime)
        {
            try
            {
                if (_ifUserExistsService.UserExistsByUser(user))
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
