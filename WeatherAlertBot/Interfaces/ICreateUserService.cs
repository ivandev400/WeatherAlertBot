using Telegram.Bot.Types;

namespace WeatherAlertBot.Interfaces
{
    public interface ICreateUserService
    {
        public void CreateUser(Update update);
    }
}
