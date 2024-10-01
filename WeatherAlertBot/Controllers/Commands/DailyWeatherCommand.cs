using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
    public class DailyWeatherCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/dailyweather";
        public string CommandDescription { get; set; }

        private WeatherService weatherService => new WeatherService();
        private string geocodingApiKey => Bot.GeocodingApiKey;
        public IGetUserService getUserService;
        public IReturnSettingsService settingsService;
        public IReplyKeyboard replyMarkup;

        public DailyWeatherCommand(IReturnSettingsService settingsService, IReplyKeyboard replyMarkup, IGetUserService getUserService)
        { 
            this.settingsService = settingsService;
            this.replyMarkup = replyMarkup;
            this.getUserService = getUserService;
        }

        public string? Recommendation = null;

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var userSettings = settingsService.ReturnSettings(update);
            var weatherResult = await weatherService.GetDailyWeatherDataResponse(userSettings, geocodingApiKey);
            var dailyWeather = weatherResult.DailyWeather;
            var hourlyWeather = weatherResult.HourlyWeather;

            var rainProbabilityText = userSettings.Language == "en" ? "Rain Probability: " : "Вірогідність опадів: ";

            var textMessage = $"MAX: {dailyWeather.MaxTemperature.FirstOrDefault()}°C \n" +
                $"MIN: {dailyWeather.MinTemperature.FirstOrDefault()}°C \n" +
                $"{rainProbabilityText}{dailyWeather.RainSum.FirstOrDefault()} \n" +
                $"MAX: {dailyWeather.MaxWindSpeed.FirstOrDefault()}km/h \n\n\n\n" +

                $"";

            await Client.SendTextMessageAsync(chatId, textMessage, replyMarkup: replyMarkup.GetPermanentMarkup(userSettings.Language));
            Recommendation = null;
        }

        public async Task SetDescription(Update update)
        {
            CommandDescription = null;
        }
    }
}
