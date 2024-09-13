using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService
    {
        private Logger<IfUserExistsService> _logger;
        private IfUserExistsService _ifUserExistsService;

        public bool ChangeUserSettingsLocation(Models.User user, string location)
        {
            try
            {
                if (_ifUserExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.Location = location;
                    return true;
                }
            }catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
            return false;
        }
        public bool ChangeUserSettingsUpdateInterval(Models.User user, string updateInterval)
        {
            try
            {
                if (_ifUserExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.UpdateInterval = updateInterval;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
            return false;
        }
        public bool ChangeUserSettingsLocation(Models.User user, TimeOnly morningTime)
        {
            try
            {
                if (_ifUserExistsService.UserExistsByUser(user))
                {
                    user.UserSettings.MorningTime = morningTime;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't change user location setting, {ex}");
            }
            return false;
        }
    }
}
