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
        public string CommandDescription {  get; set; }

        private readonly IServiceProvider _serviceProvider;
        private IGetUserService getUserService;

        public HelpCommand(IServiceProvider serviceProvider, IGetUserService getUserService)
        {
            _serviceProvider = serviceProvider;
            this.getUserService = getUserService;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);           
            string helpText = user.Language == "en" ? "📜 List of Commands: \r\n\r\n" : "📜 Список команд: \r\n\r\n";
            await SetDescription(update);

            string textMessage = helpText +
                $"{this.CommandName} - {this.CommandDescription} \r\n\r\n";

            var commands = _serviceProvider.GetServices<ICommand>();
            foreach (var command in commands)
            {
                await command.SetDescription(update);
                if (command.CommandName != "/help" && command.CommandName != "/dailyweather" && command.CommandName != "/back")
                {
                    textMessage += $"{command.CommandName} - {command.CommandDescription}\r\n\r\n";
                }
            }

            await Client.SendTextMessageAsync(chatId, textMessage);
        }

        public async Task SetDescription(Update update)
        {
            var user = getUserService.GetUser(update);
            CommandDescription = user.Language == "en" ? CommandDescriptions.HelpCommandEN : CommandDescriptions.HelpCommandUA;
        }
    }
}
