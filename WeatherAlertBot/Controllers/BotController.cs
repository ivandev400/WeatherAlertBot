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
        public TelegramBotClient _bot { get; set; }
        public BotController(IConfiguration configuration)
        {
            string botToken = configuration["botToken"];
            _bot = Bot.GetTelegramBot(botToken); 
        }

        [HttpPost]
        public void Post(Update update)
        {
            Console.WriteLine(update.Message.Text);
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }
}
