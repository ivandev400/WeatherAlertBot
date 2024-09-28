using Quartz;
using Telegram.Bot;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

public class NotificationJob : IJob
{
    private TelegramBotClient Client => Bot.GetTelegramBot();
    private UserContext userContext;
    private IMorningNotificationService notificationService;

    public NotificationJob(UserContext userContext, IMorningNotificationService notificationService)
    {
        this.userContext = userContext;
        this.notificationService = notificationService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var settings = userContext.UserSettings
            .Where(s => s.UpdateInterval == "Yes");

        foreach (var setting in settings)
        {

            string userTimeZoneId = setting.TimeZone;

            DateTime utcNow = DateTime.UtcNow;

            TimeZoneInfo userTimeZone;
            try
            {
                userTimeZone = TimeZoneConverter.TZConvert.GetTimeZoneInfo(userTimeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                userTimeZone = TimeZoneInfo.Utc;
            }

            DateTime userLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, userTimeZone);

            var user = userContext.Users
                .First(u => u.Id == setting.UserId);
            TimeOnly currentTime = new TimeOnly(userLocalTime.Hour, userLocalTime.Minute);
            TimeOnly morningTime = setting.MorningTime;

            if (currentTime.CompareTo(morningTime) == 0)
            {
                await notificationService.SendMorningNotification(user);
            }
        }
        settings = null;
    }
}
