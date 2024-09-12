using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;

namespace WeatherAlertBot.Services
{
    public class IfUserExists
    {
        private readonly UserContext _userContext;
        private Logger<IfUserExists> _logger;

        public bool UserExistsAsync(Update update)
        {
            try
            {
                if (update.Message != null)
                {
                    var userId = update.Message.Chat.Id;
                    bool isExists = _userContext.Users
                        .Any(x => x.ChatId == userId);

                    return isExists;
                }  
            }catch (Exception ex)
            {
                _logger.LogError($"Can't complete UserExistsAsync operation, {ex}");
            }
            return false;
        }
    }
}
