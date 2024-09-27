using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Services
{
    public class DailyNotifier : IDailyNotifier
    {
        private readonly UserContext userContext;
        public IMorningNotificationService notificationService;

        public DailyNotifier(UserContext userContext, IMorningNotificationService notificationService)
        {
            this.userContext = userContext;
            this.notificationService = notificationService;
        }

        public async Task SendDailyNotification()
        {
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

            TimeOnly startWindow = currentTime.AddMinutes(-1);
            TimeOnly endWindow = currentTime.AddMinutes(1);

            var settings = userContext.UserSettings
                .Where(s => s.UpdateInterval == "Yes" &&
                            s.MorningTime >= startWindow && s.MorningTime <= endWindow)
                .ToList();

            foreach (var setting in settings)
            {
                var user = userContext.Users
                    .First(u => u.Id == setting.UserId);

                if (user != null)
                {
                    await notificationService.SendMorningNotification(user);
                    await Task.Delay(TimeSpan.FromMinutes(2));
                }
            }
        }
    }
}
