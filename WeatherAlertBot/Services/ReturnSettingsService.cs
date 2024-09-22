using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;

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

            string result = $"Location: {settings.Location} \n" +
                $"Update interval: {settings.UpdateInterval} \n" +
                $"Morning time: {settings.MorningTime}";
            return result;
        }
        return "can't return settings";
    }
    public UserSettings ReturnSettings(Update update)
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

            return settings;
        }
        return null;
    }
}
