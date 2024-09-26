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
            Executor.StartListen(this);

            await Client.SendTextMessageAsync(chatId, "🌆 Дозволити боту відправляти погоду кожний ранок на задану годину? (Так/Ні)", replyMarkup: replyMarkup.GetBoolMarkup());
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

            if (update.Message.Text == "YES")
            {
                changeSettings.ChangeUserSettingsUpdateInterval(user, "Yes");

                await Client.SendTextMessageAsync(chatId, "✅ Операція успішна. Success", replyMarkup: replyMarkup.GetPermanentMarkup());
                Executor.StopListen();
                return;
            }
            changeSettings.ChangeUserSettingsUpdateInterval(user, "No");

            await Client.SendTextMessageAsync(chatId, "✅ Операція успішна. Success", replyMarkup: replyMarkup.GetPermanentMarkup());
            Executor.StopListen();
            return;
        }
    }
}
