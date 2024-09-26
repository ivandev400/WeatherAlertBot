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
        public string CommandDescription => CommandDescriptions.ChangeMorningTime;

        public IChangeUserSettingsService changeSettings;
        public IGetUserService getUserService;

        public CommandExecutor Executor { get; set; }

        public ChangeMorningTimeCommand(IChangeUserSettingsService changeSettings, IGetUserService getUserService)
        {
            this.changeSettings = changeSettings;
            this.getUserService = getUserService;
        }

        public TimeOnly MorningTime = new TimeOnly(8, 0, 0);

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            Executor.StartListen(this);

            await Client.SendTextMessageAsync(chatId, "🌆 Введіть час (8:00 приклад).Set the time (8:00 example)");
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

            if (MorningTime == new TimeOnly(8, 0, 0))
            {
                if (TimeOnly.TryParse(update.Message.Text, out TimeOnly parsedTime))
                {
                    MorningTime = parsedTime;
                }
                else
                {
                    await Client.SendTextMessageAsync(chatId, "Не вірний формат, спробуйте ще раз. Invalid format, try one more time.");
                    return;
                }
            }
            changeSettings.ChangeUserSettingsMorningTime(user, MorningTime);
            MorningTime = new TimeOnly(8, 0, 0);

            await Client.SendTextMessageAsync(chatId, "✅ Операція успішна. Success");
            Executor.StopListen();
        }
    }
}
