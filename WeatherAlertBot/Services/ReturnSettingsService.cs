using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

public class ReturnSettingsService : IReturnSettingsService
{
    private readonly UserContext userContext;
    private IUserExistsService userExistsService;

    public ReturnSettingsService(UserContext userContext, IUserExistsService userExistsService)
    {
        this.userContext = userContext;
        this.userExistsService = userExistsService;
    }

    public string ReturnSettingsToString(Update update)
    {
        if (userExistsService.UserExistsByUpdate(update) && update.Message != null)
        {
            var chatId = update.Message.Chat.Id;

            var user = userContext.Users
                .Where(x => x.ChatId == chatId)
                .First();

            var settings = userContext.UserSettings
                .Where(x => x.UserId == user.Id)
                .First();

            string result = user.Language == "en" ? $"📍Location: <b>{settings.Location}</b> \n" +
                $"🔔Notification: <b>{settings.UpdateInterval}</b> \n" +
                $"🌅Morning time: <b>{settings.MorningTime}</b>"
                :
                $"📍Місце: <b>{settings.Location}</b> \n" +
                $"🔔Сповіщення: <b>{settings.UpdateInterval}</b> \n" +
                $"🌅Ранковий час: <b>{settings.MorningTime}</b>";

            return result;
        }
        return "can't return settings";
    }
    public UserSettings ReturnSettings(Update update)
    {
        var defaultSettings = new UserSettings { Location = "Kyiv" };
        if (userExistsService.UserExistsByUpdate(update) && update.Message != null)
        {
            var chatId = update.Message.Chat.Id;
            var user = userContext.Users
                .Where(x => x.ChatId == chatId)
                .First();
            var settings = userContext.UserSettings
                .Where(x => x.UserId == user.Id)
                .First();
            return settings;
        }
        return defaultSettings;
    }
}
