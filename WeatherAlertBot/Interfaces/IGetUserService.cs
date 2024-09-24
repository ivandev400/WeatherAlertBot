using Telegram.Bot.Types;
using User = WeatherAlertBot.Models.User;

namespace WeatherAlertBot.Interfaces
{
    public interface IGetUserService
    {
        public User GetUser(Update update);
    }
}
