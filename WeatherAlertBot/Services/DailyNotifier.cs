using Telegram.Bot;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class DailyNotifier : IDailyNotifier
    {
        private TelegramBotClient Client => Bot.GetTelegramBot();
        private readonly UserContext userContext;
        public IMorningNotificationService notificationService;

        public DailyNotifier(UserContext userContext, IMorningNotificationService notificationService)
        {
            this.userContext = userContext;
            this.notificationService = notificationService;
        }

        public async Task SendDailyNotification()
        {
            while (true)
            {
                TimeOnly currentTime = new TimeOnly(DateTime.Today.Hour, DateTime.Today.Minute);

                var settings = userContext.UserSettings
                    .Where(s => s.UpdateInterval == "Yes" &&
                                s.MorningTime.Hour == currentTime.Hour && s.MorningTime.Minute == currentTime.Minute)
                    .ToList();

                foreach (var setting in settings)
                {
                    var user = userContext.Users
                        .First(u => u.Id == setting.UserId);

                    if (user != null)
                    {
                        await notificationService.SendMorningNotification(user);
                        Thread.Sleep(60000);
                    }
                }
                Thread.Sleep(5000);
            }
        }
    }
}
