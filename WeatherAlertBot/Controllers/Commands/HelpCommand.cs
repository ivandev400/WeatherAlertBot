using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Controllers.Commands
{
    public class HelpCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/help";
        public string CommandDescription => CommandDescriptions.HelpCommand;

        private readonly IServiceProvider _serviceProvider;

        public HelpCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;

            string textMessage = "Список комманд (list of commands): \r\n\r\n" +
                $"{this.CommandName} - {this.CommandDescription} \r\n\r\n";

            var commands = _serviceProvider.GetServices<ICommand>();
            foreach (var command in commands)
            {
                if (command.CommandName != "/help")
                {
                    textMessage += $"{command.CommandName} - {command.CommandDescription}\r\n\r\n";
                }
            }

            await Client.SendTextMessageAsync(chatId, textMessage);
        }
    }
}
