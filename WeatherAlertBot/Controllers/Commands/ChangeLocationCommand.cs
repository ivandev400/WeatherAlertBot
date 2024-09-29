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
        public string CommandDescription { get; set; }

        public IChangeUserSettingsService changeSettings;
        public IGetUserService getUserService;
        public IReplyKeyboard replyMarkup;

        public CommandExecutor Executor { get; set; }

        public ChangeLocationCommand(IChangeUserSettingsService changeSettings, IGetUserService getUserService, IReplyKeyboard replyMarkup)
        {
            this.changeSettings = changeSettings;
            this.getUserService = getUserService;
            this.replyMarkup = replyMarkup;
        }

        public string? location = null;

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);
            var text = user.Language == "en" ? "🌆 Send city name" : "🌆 Введіть місто";

            Executor.StartListen(this);

            await Client.SendTextMessageAsync(chatId, text);
        }
        public async Task GetUpdate(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);
            
            if (update.Message.Text == null)
            {
                return;
            }

            if (user == null)
            {
                var warning = user.Language == "en" ? "☢️ Error, try start command" : "☢️ Вас нема в базі даних, спробуйте команду /start";
                await Client.SendTextMessageAsync(chatId, warning);
                Executor.StopListen();
                return;
            }

            if (location == null)
            {
                location = update.Message.Text;
            }
            changeSettings.ChangeUserSettingsLocation(user, location);
            location = null;

            var text = user.Language == "en" ? "✅ Success" : "✅ Операція успішна";
            await Client.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup.GetPermanentMarkup(user.Language));
            Executor.StopListen();
        }

        public async Task SetDescription(Update update)
        {
            var user = getUserService.GetUser(update);
            CommandDescription = user.Language == "en" ? CommandDescriptions.ChangeLocationEN : CommandDescriptions.ChangeLocationUA;
        }
    }
}
