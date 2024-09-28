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
        private UpdateDistributor<CommandExecutor> updateDistributor;

        public BotController(UpdateDistributor<CommandExecutor> updateDistributor)
        {
            this.updateDistributor = updateDistributor;
        }

        [HttpPost]
        public async void Post(Update update)
        {
            if (update.Message == null)
            {
                return;
            }
            switch (update.Message.Text)
            {
                case "Змінити місце || Change Location":
                    update.Message.Text = "/changelocation";
                    break;
                case "Змінити час ранкового сповіщення || Change notification morning time":
                    update.Message.Text = "/changemorningtime";
                    break;
            }

            await updateDistributor.GetUpdate(update);
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }
}
