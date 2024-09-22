using Telegram.Bot;
using WeatherAlertBot.Models;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
    public class StartCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/start";
        public string CommandDescription => CommandDescriptions.StartCommandDescription;
        public ICreateUserService createUserService;

        public StartCommand(ICreateUserService createUserService)
        {
            this.createUserService = createUserService;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            string result = createUserService.CreateUser(update);

            await Client.SendTextMessageAsync(chatId, result);
        }
    }
}
