using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;
using User = WeatherAlertBot.Models.User;

namespace WeatherAlertBot.Controllers.Commands
{
    public class ChangeLocationCommand : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/changelocation";
        public string CommandDescription => null;

        public IChangeUserSettingsService changeSettings;
        public IGetUserService getUserService;

        public CommandExecutor Executor { get; set; }

        public ChangeLocationCommand(IChangeUserSettingsService changeSettings, IGetUserService getUserService)
        {
            this.changeSettings = changeSettings;
            this.getUserService = getUserService;
        }

        public string? location = null;
        public User User 

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            User = getUserService.GetUser(update);

            Executor.StartListen(this);

            await Client.SendTextMessageAsync(chatId, "Введіть місто. Send city name");
        }
        public async Task GetUpdate(Update update)
        {
            long chatId = update.Message.Chat.Id;
            if (update.Message.Text == null)
            {
                return;
            }
            var user = getUserService.GetUser(update);

            if (user == null)
            {
                await Client.SendTextMessageAsync(chatId, "Вас нема в базі даних, спробуйте команду /start. Error, try start command.");
                Executor.StopListen();
                return;
            }

            if (location == null)
            {
                location = update.Message.Text;
            }
            else
            {
                changeSettings.ChangeUserSettingsLocation(user, location);
                await Client.SendTextMessageAsync(chatId, "Операція успішна. Success");
                Executor.StopListen();
            }
        }
    }
}
