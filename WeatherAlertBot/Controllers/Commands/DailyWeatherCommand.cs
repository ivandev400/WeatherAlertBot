using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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

        public IWeatherPlotService plotService;
        public IGetUserService getUserService;
        public IReturnSettingsService settingsService;
        public IReplyKeyboard replyMarkup;

        public DailyWeatherCommand(IWeatherPlotService plotService, IReturnSettingsService settingsService, IReplyKeyboard replyMarkup, IGetUserService getUserService)
        { 
            this.plotService = plotService;
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

            var morningData = FilterTimeRange(hourlyWeather, 5, 11);
            string morningText = userSettings.Language == "en" ? "MORNING" : "РАНОК";
            var afternoonData = FilterTimeRange(hourlyWeather, 12, 16);
            string afternoonText = userSettings.Language == "en" ? "AFTERNOON" : "ОБІД";
            var eveningData = FilterTimeRange(hourlyWeather, 17, 24);
            string eveningText = userSettings.Language == "en" ? "EVENING" : "ВЕЧІР";

            var rainSumText = userSettings.Language == "en" ? "Rain Sum: " : "Сума опадів: ";
            string introductionText = userSettings.Language == "en" ? "🌤️ THE DAILY WHEATHER" : "🌤️ ПОГОДА НА ДЕНЬ";
            var textMessage = $" \n" +
                $"MAX: 🌡️ <b>{dailyWeather.MaxTemperature.FirstOrDefault()}°C </b>\n" +
                $"MIN: 🌡️ <b>{dailyWeather.MinTemperature.FirstOrDefault()}°C </b>\n" +
                $"{rainSumText}☔<b>{dailyWeather.RainSum.FirstOrDefault()}mm </b>\n" +
                $"MAX: 💨 <b>{dailyWeather.MaxWindSpeed.FirstOrDefault()}km/h </b>\n\n" +

                $"{morningText}: \n" +
                $"🌡️ <b>{Math.Round(morningData.Average(x => x.temperature), 1)}°C </b>\n" +
                $"🌩️ <b>{Math.Round(morningData.Average(x => x.rain), 1)}mm </b>\n" +
                $"🍃 <b>{Math.Round(morningData.Average(x => x.windSpeed), 1)}km/h </b>\n\n" +

                $"{afternoonText}: \n" +
                $"🌡️ <b>{Math.Round(afternoonData.Average(x => x.temperature), 1)}°C </b>\n" +
                $"🌩️ <b>{Math.Round(afternoonData.Average(x => x.rain), 1)}mm </b>\n" +
                $"🍃 <b>{Math.Round(afternoonData.Average(x => x.windSpeed), 1)}km/h </b>\n\n" +

                $"{eveningText}: \n" +
                $"🌡️ <b>{Math.Round(eveningData.Average(x => x.temperature), 1)}°C </b>\n" +
                $"🌩️ <b>{Math.Round(eveningData.Average(x => x.rain), 1)}mm </b>\n" +
                $"🍃 <b>{Math.Round(eveningData.Average(x => x.windSpeed), 1)}km/h </b>\n\n";

            await Client.SendTextMessageAsync(chatId, textMessage, null, ParseMode.Html, replyMarkup: replyMarkup.GetPermanentMarkup(userSettings.Language));
            await plotService.GenerateWeatherPlot(Client, hourlyWeather, update, userSettings.Language);
            Recommendation = null;
        }

        public async Task SetDescription(Update update)
        {
            CommandDescription = null;
        }

        private IEnumerable<(double temperature, double rain, double windSpeed)> FilterTimeRange(HourlyWeather hourly, int startHour, int endHour)
        {
            var filteredData = hourly.Hours
                .Where(h => h.Hour >= startHour && h.Hour <= endHour)
                .Select(h => (
                temperature: hourly.Temperature[hourly.Hours.IndexOf(h)],
                rain: hourly.Rain[hourly.Hours.IndexOf(h)],
                windSpeed: hourly.WindSpeed[hourly.Hours.IndexOf(h)]
            ));

            return filteredData;
        }
    }
}
