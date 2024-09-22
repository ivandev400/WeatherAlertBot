using Telegram.Bot.Types;

namespace WeatherAlertBot.Interfaces
{
    public interface IGetUserService
    {
        public Models.User GetUser(Update update);
    }
}
