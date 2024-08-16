using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers
{
    [Route("/")]
    [ApiController]
    public class BotController : ControllerBase
    {
        public TelegramBotClient bot { get; set; }
        public WeatherService weatherService { get; set; }
        public string geocodingApiKey { get; set; }
        public BotController(IConfiguration configuration)
        {
            string botToken = configuration["botToken"];
            geocodingApiKey = configuration["GeocodingApiKey"];
            bot = Bot.GetTelegramBot(botToken);
            weatherService = new WeatherService();
        }

        [HttpPost]
        public async void Post(Update update)
        {
            Console.WriteLine(update.Message.Text);
            var userSettings = new UserSettings { Location = "Kyiv" };
            var weatherResult = await weatherService.GetWeatherDataStringResponse(userSettings, geocodingApiKey);

            string message = $"Time: {weatherResult.Time}\n" +
                             $"Temperature: {weatherResult.Temperature}°C\n" +
                             $"Rain(mm): {weatherResult.Rain}\n" +
                             $"Wind speed(km/h): {weatherResult.WindSpeed}\n";

            await bot.SendTextMessageAsync(update.Message.Chat.Id, message);
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }
}
