using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService
    {
        private readonly UserContext _userContext;
        private Logger<IfUserExistsService> _logger;
        private IfUserExistsService _ifUserExistsService;

        public bool ChangeUserSettings(Models.User user, string location, string updateInterval, TimeOnly morningTime)
        {
            if (_ifUserExistsService.UserExistsByUser(user))
            {

            }
        }
    }
}
