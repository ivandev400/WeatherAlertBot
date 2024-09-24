using Telegram.Bot.Types;
using WeatherAlertBot.Controllers.Commands;

namespace WeatherAlertBot.Interfaces
{
    public interface IListener
    {
        public Task GetUpdate(Update update);

        public CommandExecutor Executor { get; set; }
    }
}
