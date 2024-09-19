using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;

namespace WeatherAlertBot.Services
{
    public class IfUserExistsService
    {
        private readonly UserContext _userContext;
        private Logger<IfUserExistsService> _logger;

        public IfUserExistsService(UserContext userContext, Logger<IfUserExistsService> logger)
        {
            _userContext = userContext;
            _logger = logger;
        }

        public bool UserExistsByUpdate(Update update)
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
        public bool UserExistsByUser(Models.User user)
        {
            try
            {
                if (user != null)
                {
                    var userId = user.ChatId;
                    bool isExists = _userContext.Users
                        .Any(x => x.ChatId == userId);

                    return isExists;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't complete UserExistsAsync operation, {ex}");
            }
            return false;
        }
    }
}
