using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Db;
using WeatherAlertBot.Interfaces;
using Telegram.Bot.Types;

namespace WeatherAlertBot.Services
{
    public class MorningNotificationService : BackgroundService
    {
        private readonly UserContext userContext;
        public CurrentWeatherCommand CurrentWeatherCommand;
        public DailyWeatherCommand DailyWeatherCommand;

        public MorningNotificationService(UserContext userContext, IGetUserService getUserService, IReturnSettingsService settingsService, IReplyKeyboard replyMarkup)
        {
            this.userContext = userContext;
            CurrentWeatherCommand = new CurrentWeatherCommand(settingsService, replyMarkup);
            DailyWeatherCommand = new DailyWeatherCommand(settingsService, replyMarkup, getUserService);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndSendMorningNotifications();
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }

        private async Task CheckAndSendMorningNotifications()
        {
            var users = userContext.Users.ToList();

            foreach (var user in users)
            {
                var settings = userContext.UserSettings
                    .FirstOrDefault(u => u.UserId == user.Id);

                if (settings == null) continue;

                TimeOnly morningTime = settings.MorningTime;
                string updateInterval = settings.UpdateInterval;

                if (updateInterval == "Yes")
                {
                    TimeOnly.TryParse($"{DateTime.Now}", out TimeOnly currentTime);
                    if (currentTime.Hour == morningTime.Hour && Math.Abs(currentTime.Minute - morningTime.Minute) <= 2)
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
                    }
                }
            }
        }
    }
}
