using WeatherAlertBot.Interfaces;
using Telegram.Bot.Types;

namespace WeatherAlertBot.Controllers.Commands
{
    public class CommandExecutor : ITelegramUpdateListener
    {
        private readonly IEnumerable<ICommand> _commands;
        private IListener? listener = null;

        public CommandExecutor(IEnumerable<ICommand> commands)
        {
            _commands = commands;
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
            foreach (var command in _commands)
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
