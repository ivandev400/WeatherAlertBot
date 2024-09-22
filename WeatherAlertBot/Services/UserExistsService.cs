using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Services
{
    public class UserExistsService : IUserExistsService
    {
        private readonly UserContext userContext;
        private Logger<UserExistsService> logger;

        public UserExistsService(UserContext userContext, Logger<UserExistsService> logger)
        {
            this.userContext = userContext;
            this.logger = logger;
        }

        public bool UserExistsByUpdate(Update update)
        {
            try
            {
                if (update.Message != null)
                {
                    var userId = update.Message.Chat.Id;
                    bool isExists = userContext.Users
                        .Any(x => x.ChatId == userId);

                    return isExists;
                }  
            }catch (Exception ex)
            {
                logger.LogError($"Can't complete UserExistsAsync operation, {ex}");
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
                    bool isExists = userContext.Users
                        .Any(x => x.ChatId == userId);

                    return isExists;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Can't complete UserExistsAsync operation, {ex}");
            }
            return false;
        }
    }
}
