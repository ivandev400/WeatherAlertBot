using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Controllers.Commands
{
    public class ChangeLocationCommand : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/changelocation";
        public string CommandDescription => null;
        public IChangeUserSettingsService changeSettings;
        public IGetUserService getUserService;
        public CommandExecutor Executor { get; }

        public ChangeLocationCommand(IChangeUserSettingsService changeSettings, IGetUserService getUserService, CommandExecutor executor)
        {
            this.changeSettings = changeSettings;
            Executor = executor;
        }

        private string? location = null;

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            Executor.StartListen(this);
            await Client.SendTextMessageAsync(chatId, "Введіть місто. Send city name");
        }
        public async Task GetUpdate(Update update)
        {
            long chatId = update.Message.Chat.Id;

            if (update.Message == null)
            {
                return;
            }

            var user = getUserService.GetUser(update);
            
            if (location == null)
            {
                location = update.Message.Text;
            }else
            {
                changeSettings.ChangeUserSettingsLocation(user, location);
                await Client.SendTextMessageAsync(chatId, "Операція успішна. Success");
                Executor.StopListen();
            }
        }
    }
}
