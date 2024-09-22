using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class CreateUserService : ICreateUserService
    {
        private readonly UserContext userContext;
        private IUserExistsService userExistsService;

        public CreateUserService(UserContext userContext, IUserExistsService userExistsService)
        {
            this.userContext = userContext;
            this.userExistsService = userExistsService;
        }

        public string CreateUser(Update update)
        {
            if (!userExistsService.UserExistsByUpdate(update))
            {
                var newUser = new Models.User
                {
                    ChatId = update.Message.Chat.Id,
                    UserSettings = new UserSettings
                    {
                        UserID = update.Message.Chat.Id,
                        Location = "Kyiv",
                        UpdateInterval = "",
                        MorningTime = new TimeOnly(8, 0, 11),
                    }
                };

                userContext.Users.Add(newUser);
                userContext.SaveChanges();

                return "I'm here";
            }
            return "something wrong :(";
        }
    }
}
