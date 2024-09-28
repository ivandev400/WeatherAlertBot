using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
    public class LanguageCommand : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/language";
        public string CommandDescription => CommandDescriptions.LanguageCommand;
        public IReplyKeyboard replyMarkup;
        public IGetUserService getUserService;
        public IChangeUserSettingsService changeSettingsService;

        public CommandExecutor Executor { get; set; }

        public LanguageCommand( IReplyKeyboard replyMarkup, IGetUserService getUserService, IChangeUserSettingsService changeSettingsService)
        {
            this.replyMarkup = replyMarkup;
            this.getUserService = getUserService;
            this.changeSettingsService = changeSettingsService;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;

            Executor.StartListen(this);

            await Client.SendTextMessageAsync(chatId, "Виберіть мову. Choose language.", replyMarkup: replyMarkup.GetLanguageMarkup());
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
                await Client.SendTextMessageAsync(chatId, "☢️ Вас нема в базі даних, спробуйте команду /start. Error, try start command.");
                Executor.StopListen();
                return;
            }

            if (update.Message.Text == "🇬🇧EN🇬🇧")
            {
                changeSettingsService.ChangeLanguage(user, "en");

                await Client.SendTextMessageAsync(chatId, "✅ Success", replyMarkup: replyMarkup.GetPermanentMarkup("en"));
                Executor.StopListen();
                return;
            }
            if (update.Message.Text == "🇺🇦UA🇺🇦")
            {
                changeSettingsService.ChangeLanguage(user, "ua");

                await Client.SendTextMessageAsync(chatId, "✅ Операція успішна", replyMarkup: replyMarkup.GetPermanentMarkup("ua"));
                Executor.StopListen();
                return;
            }
        }
    }
}
