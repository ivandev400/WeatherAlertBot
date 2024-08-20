using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers
{
    [Route("/")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private Bot bot {  get; set; }
        private TelegramBotClient botClient { get; set; }
        public WeatherService weatherService { get; set; }
        private string geocodingApiKey { get; set; }
        public BotController(IConfiguration configuration)
        {
            geocodingApiKey = configuration["GeocodingApiKey"];
            bot = new Bot(configuration);
            botClient = Bot.GetTelegramBot();
            weatherService = new WeatherService();
        }
        private UpdateDistributor<CommandExecutor> updateDistributor = new UpdateDistributor<CommandExecutor>();

        [HttpPost]
        public async void Post(Update update)
        {
            if (update.Message == null)
            {
                return;
            }

            await updateDistributor.GetUpdate(update);

            /*Console.WriteLine(update.Message.Text);
            var userSettings = new UserSettings { Location = "Kyiv" };
            var weatherResult = await weatherService.GetWeatherDataStringResponse(userSettings, geocodingApiKey);

            string message = $"Time: {weatherResult.Time.ToString("HH:mm")}\n" +
                             $"Temperature: {weatherResult.Temperature}°C\n" +
                             $"Rain(mm): {weatherResult.Rain}\n" +
                             $"Wind speed(km/h): {weatherResult.WindSpeed}\n";

            await bot.SendTextMessageAsync(update.Message.Chat.Id, message);*/
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }
}
