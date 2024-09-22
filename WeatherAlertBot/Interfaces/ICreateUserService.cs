using Telegram.Bot.Types;

namespace WeatherAlertBot.Interfaces
{
    public interface ICreateUserService
    {
        public string CreateUser(Update update);
    }
}
