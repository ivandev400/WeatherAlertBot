using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;

public class ReturnSettingsService : IReturnSettingsService
{
    private readonly UserContext _userContext;
    private IUserExistsService _userExistsService;

    public ReturnSettingsService(UserContext userContext, IUserExistsService userExistsService)
    {
        _userContext = userContext;
        _userExistsService = userExistsService;
    }

    public string ReturnSettings(Update update)
    {
        if (_userExistsService.UserExistsByUpdate(update) && update.Message != null)
        {
            var chatId = update.Message.Chat.Id;
            var user = _userContext.Users
                .Where(x => x.ChatId == chatId)
                .First();

            string result = $"Location: {user.UserSettings.Location} \n" +
                $"Update interval: {user.UserSettings.UpdateInterval} \n" +
                $"Morning time: {user.UserSettings.MorningTime}";
            return result;
        }
        return "can't return settings";
    }
}
