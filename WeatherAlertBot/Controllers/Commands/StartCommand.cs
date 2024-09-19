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

        public CreateUserService CreateUserService {  get; set; }
        public IfUserExistsService IfUserExistsService { get; set; }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            if (IfUserExistsService.UserExistsByUpdate(update) == false)
            {
                CreateUserService.CreateUser(update);
            }

            await Client.SendTextMessageAsync(chatId, CommandDescription);
        }
    }
}
