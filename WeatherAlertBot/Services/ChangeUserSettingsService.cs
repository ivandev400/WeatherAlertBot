using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;

namespace WeatherAlertBot.Services
{
    public class ChangeUserSettingsService
    {
        private readonly UserContext _userContext;
        private Logger<IfUserExistsService> _logger;
    }
}
