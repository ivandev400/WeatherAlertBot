using Telegram.Bot.Types;
using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Interfaces;
using User = WeatherAlertBot.Models.User;

namespace WeatherAlertBot.Services
{
    public class MorningNotificationService : IMorningNotificationService
    {
        public CurrentWeatherCommand CurrentWeatherCommand;
        public DailyWeatherCommand DailyWeatherCommand;

        public MorningNotificationService(IGetUserService getUserService, IReturnSettingsService settingsService, IReplyKeyboard replyMarkup)
        {
            CurrentWeatherCommand = new CurrentWeatherCommand(settingsService, replyMarkup, getUserService);
            DailyWeatherCommand = new DailyWeatherCommand(settingsService, replyMarkup, getUserService);
        }

        public async Task SendMorningNotification(User user)
        {
            Update update = new Update
            {
                Message = new Message
                {
                    Chat = new Chat
                    {
                        Id = user.ChatId
                    }
                }
            };

            await CurrentWeatherCommand.Execute(update);
            await DailyWeatherCommand.Execute(update);
        }
    }
}
