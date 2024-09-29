using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Controllers.Commands
{
    public class AnableNotificationCommand : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/anablenotification";
        public string CommandDescription => CommandDescriptions.AnableNotification;

        public IChangeUserSettingsService changeSettings;
        public IGetUserService getUserService;
        public IReplyKeyboard replyMarkup;

        public CommandExecutor Executor { get; set; }

        public AnableNotificationCommand(IChangeUserSettingsService changeSettings, IGetUserService getUserService, IReplyKeyboard replyMarkup)
        {
            this.changeSettings = changeSettings;
            this.getUserService = getUserService;
            this.replyMarkup = replyMarkup;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);
            var text = user.Language == "en" ? "🌆 Allow to the bot to send you morning notification every day?(YES/NO)" : "🌆 Дозволити боту відправляти погоду кожний ранок на задану годину? (ТАК/НІ)";

            Executor.StartListen(this);

            await Client.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup.GetBoolMarkup(user.Language));
        }
        public async Task GetUpdate(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);
            var text = user.Language == "en" ? "✅ Success" : "✅ Операція успішна";

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

            if (update.Message.Text == "YES" || update.Message.Text == "ТАК")
            {
                changeSettings.ChangeUserSettingsUpdateInterval(user, "Yes");

                await Client.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup.GetPermanentMarkup(user.Language));
                Executor.StopListen();
                return;
            }
            changeSettings.ChangeUserSettingsUpdateInterval(user, "No");

            await Client.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup.GetPermanentMarkup(user.Language));
            Executor.StopListen();
            return;
        }
    }
}
