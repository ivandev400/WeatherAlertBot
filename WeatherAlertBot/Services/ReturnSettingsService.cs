using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot.Models;

public class ReturnSettingsService
{
    private readonly UserContext _userContext;
    private Logger<IfUserExistsService> _logger;
    private IfUserExistsService _userExistsService;

    public string ReturnSettings(Models.Update update)
    {
        if (_userExistsService.UserExists(update) && update.Message != null)
        {
            var user = _userContext.User
                .Where(x => x.ChatId == update.Chat.Id);

            string result = $"Location: {user.UserSettings.Location} \n" +
                $"Update interval: {user.UserSettings.UpdateInterval} \n" +
                $"Morning time: {user.UserSettings.MorningTime}";
            return result;
        }
        return;
    }
}
