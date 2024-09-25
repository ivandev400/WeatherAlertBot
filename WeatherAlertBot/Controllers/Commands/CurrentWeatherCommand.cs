using Telegram.Bot.Types;
using Telegram.Bot;
using WeatherAlertBot.Models;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
    public class CurrentWeatherCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/currentweather";
        public string CommandDescription => CommandDescriptions.CurrentWeather;

        private WeatherService weatherService => new WeatherService();
        private string geocodingApiKey => Bot.GeocodingApiKey;
        public IReturnSettingsService settingsService;

        public CurrentWeatherCommand(IReturnSettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public string? Recommendation = null; 

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var userSettings = settingsService.ReturnSettings(update);
            var weatherResult = await weatherService.GetWeatherDataStringResponse(userSettings, geocodingApiKey);

            string message = $"⌚   {weatherResult.Time.ToString("HH:mm")}\n" +
                             $"🌡️   {weatherResult.Temperature}°C\n" +
                             $"🌩️   {RainConverter(weatherResult.Rain)}\n" +
                             $"🍃   {weatherResult.WindSpeed} km/h\r\n\r\n" +
                             Recommendation;

            await Client.SendTextMessageAsync(update.Message.Chat.Id, message);
            Recommendation = null;
        }
        private string RainConverter(double rain)
        {
            switch (rain)
            {
                case <= 0.5:
                    Recommendation += "Можна не боятися намокнути ©️ \n";
                    return "Дощу нема 🌤️";
                case > 0.5 and <= 2:
                    Recommendation += "Щось таки треба вдягнути ©️ \n";
                    return "Моросить 💧";
                case > 2 and <= 6:
                    Recommendation += "Тут точно треба парасоля ©️ \n";
                    return "Середній дощ ☔";
                case > 6 and <= 10:
                    Recommendation += "Треба бути окуратний, тут парасоля мало чим допоможе ©️ \n";
                    return "Сильний дощ 🌧️";
                case > 10 and <= 18:
                    Recommendation += "Без коментарів, рекомендую залишитись вдома ©️ \n";
                    return "Дуже сильний дощ 😶‍🌫";
                case > 18:
                    Recommendation += "Ну тут без коментарів, це смерть... ©️ \n";
                    return "Л'є як із відра, найсьльніший дощ ⚠️⚠️";
            }
            return "";
        }
    }
}
