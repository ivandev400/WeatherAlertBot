using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Controllers
{
    [Route("/")]
    [ApiController]
    public class BotController : ControllerBase
    {
        public TelegramBotClient bot { get; set; }
        public APILink apiLink { get; set; }
        public BotController(IConfiguration configuration)
        {
            string botToken = configuration["botToken"];
            bot = Bot.GetTelegramBot(botToken);
            apiLink = new APILink();
        }

        [HttpPost]
        public async void Post(Update update)
        {
            Console.WriteLine(update.Message.Text);
            var userSettings = new UserSettings { Location = "Kyiv" };
            string weatherData = await apiLink.GetWeatherDataStringResponse(userSettings);

            await bot.SendTextMessageAsync(update.Message.Chat.Id, weatherData);
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }
}
