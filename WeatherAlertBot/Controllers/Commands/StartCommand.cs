using Telegram.Bot;
using WeatherAlertBot.Models;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using Supabase.Gotrue;

namespace WeatherAlertBot.Controllers.Commands
{
    public class StartCommand : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/start";
        public string CommandDescription => CommandDescriptions.StartCommand;
        public CommandExecutor Executor { get; set; }

        public ICreateUserService createUserService;
        public IReplyKeyboard replyMarkup;
        public IGetUserService getUserService;
        public IChangeUserSettingsService changeSettingsService;

        public StartCommand(ICreateUserService createUserService, IReplyKeyboard replyMarkup, IGetUserService getUserService, IChangeUserSettingsService changeSettingsService)
        {
            this.createUserService = createUserService;
            this.replyMarkup = replyMarkup;
            this.getUserService = getUserService;
            this.changeSettingsService = changeSettingsService;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            createUserService.CreateUser(update);
            var user = getUserService.GetUser(update);
            var text = user.Language == "en" ? "Choose language" : "Виберіть мову";

            Executor.StartListen(this);
            await Client.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup.GetLanguageMarkup());
        }

        public async Task GetUpdate(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);

            if (update.Message.Text == null)
            {
                return;
            }

            if (update.Message.Text == "🇬🇧EN🇬🇧")
            {
                changeSettingsService.ChangeLanguage(user, "en");

                await Client.SendTextMessageAsync(chatId, "✅ Success", replyMarkup: replyMarkup.GetPermanentMarkup("en"));
            }
            if (update.Message.Text == "🇺🇦UA🇺🇦")
            {
                changeSettingsService.ChangeLanguage(user, "ua");

                await Client.SendTextMessageAsync(chatId, "✅ Операція успішна", replyMarkup: replyMarkup.GetPermanentMarkup("ua"));
            }

            string textMessage = user.Language == "en" ? "This is Weather Alert Bot(or storm watch).This Bot was created to help you to know weather at time. Here you can use different commands shown below - immidiately send you message with weather info in location that you can see and set using command /settings (default location is Kyiv)." : "Це Weather Alert Bot (або Storm Watch). Цей бот був створений, щоб допомогти вам дізнатися погоду в даний момент у будь-якій точці світу та інші плюшки. Тут ви можете використовувати різні команди показані нижче.Бот може негайно надіслати вам повідомлення з інформацією про погоду в будь-якій точці світу, яке ви можете переглянути та вказати за допомогою команди /settings (за замовчуванням - Київ).";

            await Client.SendTextMessageAsync(chatId, textMessage, replyMarkup: replyMarkup.GetPermanentMarkup(user.Language));

            Executor.StopListen();
        }
    }
}
