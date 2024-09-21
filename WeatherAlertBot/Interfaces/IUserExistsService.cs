using Telegram.Bot.Types;
using User = WeatherAlertBot.Models.User;

namespace WeatherAlertBot.Interfaces
{
    public interface IUserExistsService
    {
        public bool UserExistsByUpdate(Update update);
        public bool UserExistsByUser(User user);
    }
}
