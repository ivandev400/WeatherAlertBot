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
        public string CommandDescription => null;

        private WeatherService weatherService => new WeatherService();
        private string geocodingApiKey => Bot.GeocodingApiKey;
        public IReturnSettingsService settingsService;

        public CurrentWeatherCommand(IReturnSettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var userSettings =settingsService.ReturnSettings(update);
            var weatherResult = await weatherService.GetWeatherDataStringResponse(userSettings, geocodingApiKey);

            string message = $"Time: {weatherResult.Time.ToString("HH:mm")}\n" +
                             $"Temperature: {weatherResult.Temperature}°C\n" +
                             $"Rain(mm): {weatherResult.Rain}\n" +
                             $"Wind speed(km/h): {weatherResult.WindSpeed}\n";

            await Client.SendTextMessageAsync(update.Message.Chat.Id, message);
        }
    }
}
