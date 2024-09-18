using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Db;
using WeatherAlertBot;
using WeatherAlertBot.Services;
using Telegram.Bot.Types.Enums;

namespace WeatherAlertBot.Services
{
    public class ReturnSettingsService
    {
        private readonly UserContext _userContext;
        private Logger<IfUserExistsService> _logger;
        private IfUserExistsService _userExistsService;

        public string ReturnSettings(Update update)
        {
            if (_userExistsService.UserExistsByUpdate(update))
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
}
