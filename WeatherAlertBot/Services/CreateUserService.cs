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

        public void CreateUser(Update update)
        {
            if (!userExistsService.UserExistsByUpdate(update))
            {
                var newUserSettings = new UserSettings
                {
                    Location = "Kyiv",
                    UpdateInterval = "",
                    MorningTime = new TimeOnly(8, 0),
                    TimeZone = "Europe/Kiev"
                };

                var newUser = new Models.User
                {
                    ChatId = update.Message.Chat.Id,
                    UserSettings = newUserSettings
                };
                userContext.Users.Add(newUser);
                userContext.SaveChanges();
            }
            return;
        }
    }
}
