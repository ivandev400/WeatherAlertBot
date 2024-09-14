using WeatherAlertBot.Interfaces;
using Telegram.Bot.Types;

namespace WeatherAlertBot.Controllers.Commands
{
    public class CommandExecutor : ITelegramUpdateListener
    {
        private List<ICommand> commands;
        private IListener? listener = null;

        public CommandExecutor()
        {
            commands = new List<ICommand>
            {
                new StartCommand(),
                new CurrentWeatherCommand(),
                new SettingsCommand()
            };
        }
        public async Task GetUpdate(Update update)
        {
            if (listener == null)
            {
                await ExecuteCommand(update);
            }
            else
            {
                await listener.GetUpdate(update);
            }
        }

        private async Task ExecuteCommand(Update update)
        {
            Message message = update.Message;
            foreach (var command in commands)
            {
                if (command.CommandName == message.Text)
                {
                    await command.Execute(update);
                }
            }
        }

        public void StartListen(IListener newlistener)
        {
            listener = newlistener;
        }
        public void StopListen()
        {
            listener = null;
        }
    }
}
