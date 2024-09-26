using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using Telegram.Bot.Types;
using User = WeatherAlertBot.Models.User;

namespace WeatherAlertBot.Services
{
    public class MorningNotificationService : IMorningNotificationService
    {
        private readonly UserContext userContext;
        public CurrentWeatherCommand currentWeatherCommand;
        public DailyWeatherCommand dailyWeatherCommand;

        public MorningNotificationService(CurrentWeatherCommand currentWeatherCommand, DailyWeatherCommand dailyWeatherCommand)
        {
            this.currentWeatherCommand = currentWeatherCommand;
            this.dailyWeatherCommand = dailyWeatherCommand;
        }

        public async Task GetMorningNotification(User user, Update update)
        {
            if (user != null)
            {
                var chatId = user.ChatId;
                var settings = userContext.UserSettings
                    .First(u => u.UserId == user.Id);

                TimeOnly morningTime = settings.MorningTime;
                string updateInterval = settings.UpdateInterval;

                while (updateInterval == "Yes")
                {
                    TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

                    if (currentTime.Hour ==  morningTime.Hour && currentTime.Minute == morningTime.Minute)
                    {
                        var newUpdate = update;
                        await currentWeatherCommand.Execute(update);
                        await dailyWeatherCommand.Execute(update);

                        await Task.Delay(TimeSpan.FromMinutes(1));
                    }
                    await Task.Delay(TimeSpan.FromSeconds(30));
                }
                return;
            } else
            {
                return;
            }
        }
    }
}
