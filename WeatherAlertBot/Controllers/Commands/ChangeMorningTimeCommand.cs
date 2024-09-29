using Supabase.Gotrue;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Controllers.Commands
{
    public class ChangeMorningTimeCommand : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/changemorningtime";
        public string CommandDescription {  get; set; }

        public IChangeUserSettingsService changeSettings;
        public IGetUserService getUserService;
        public IReplyKeyboard replyMarkup;

        public CommandExecutor Executor { get; set; }

        public ChangeMorningTimeCommand(IChangeUserSettingsService changeSettings, IGetUserService getUserService, IReplyKeyboard replyMarkup)
        {
            this.changeSettings = changeSettings;
            this.getUserService = getUserService;
            this.replyMarkup = replyMarkup;
        }

        public TimeOnly MorningTime = new TimeOnly(8, 0);

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);
            var text = user.Language == "en" ? "🌆 Set the time (8:00 for example)" : "🌆 Введіть час (8:00 наприклад)";
            Executor.StartListen(this);

            await Client.SendTextMessageAsync(chatId, text);
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

            if (MorningTime == new TimeOnly(8, 0))
            {
                if (TimeOnly.TryParse(update.Message.Text, out TimeOnly parsedTime))
                {
                    MorningTime = parsedTime;
                }
                else
                {
                    var warning = user.Language == "en" ? "Invalid format, try one more time." : "Не вірний формат, спробуйте ще раз.";
                    await Client.SendTextMessageAsync(chatId, warning);
                    return;
                }
            }
            changeSettings.ChangeUserSettingsMorningTime(user, MorningTime);
            MorningTime = new TimeOnly(8, 0);

            await Client.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup.GetPermanentMarkup(user.Language));
            Executor.StopListen();
        }

        public async Task SetDescription(Update update)
        {
            var user = getUserService.GetUser(update);
            CommandDescription = user.Language == "en" ? CommandDescriptions.ChangeMorningTimeEN : CommandDescriptions.ChangeMorningTimeUA;
        }
    }
}
