using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Services
{
    public class GetUserService : IGetUserService
    {
        private readonly UserContext userContext;
        private Logger<UserExistsService> logger;
        private IUserExistsService userExistsService;

        public GetUserService(UserContext userContext, Logger<UserExistsService> logger, IUserExistsService userExistsService)
        {
            this.userContext = userContext;
            this.logger = logger;
            this.userExistsService = userExistsService;
        }

        public Models.User GetUser(Update update)
        {
            if (userExistsService.UserExistsByUpdate(update))
            {
                long chatId = update.Message.Chat.Id;

                var user = userContext.Users
                    .Where(user => user.ChatId == chatId)
                    .First();

                return user;
            }
            return null;
        }
    }
}
