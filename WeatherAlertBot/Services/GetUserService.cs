using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using User = WeatherAlertBot.Models.User;

namespace WeatherAlertBot.Services
{
    public class GetUserService : IGetUserService
    {
        private readonly UserContext userContext;
        private IUserExistsService userExistsService;
        private ICreateUserService createUserService;

        public GetUserService(UserContext userContext, IUserExistsService userExistsService,ICreateUserService createUserService)
        {
            this.userContext = userContext;
            this.userExistsService = userExistsService;
            this.createUserService = createUserService;
        }

        public User GetUser(Update update)
        {
            long chatId = update.Message.Chat.Id;

            if (userExistsService.UserExistsByUpdate(update))
            { 
                var user = userContext.Users
                    .Where(user => user.ChatId == chatId)
                    .First();
                return user;
            }
            return null;
        }
    }
}
