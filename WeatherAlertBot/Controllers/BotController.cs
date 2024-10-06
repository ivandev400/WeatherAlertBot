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
                case "Змінити місце":
                    case "Change Location":
                    update.Message.Text = "/changelocation";
                    break;

                case "Змінити час ранкового сповіщення":
                    case "Change notification morning time":
                    update.Message.Text = "/changemorningtime";
                    break;

                case "Current Weather":
                    case "Поточна погода":
                    update.Message.Text = "/currentweather";
                    break;

                case "Anable Morning Notifications":
                    case "Дозволити Ранкові Сповіщення":
                    update.Message.Text = "/anablenotification";
                    break;

                case "Settings":
                    case "Налаштування":
                    update.Message.Text = "/settings";
                    break;

                case "Language":
                    case "Мова":
                    update.Message.Text = "/language";
                    break;

                case "Help":
                    case "Допомогти":
                    update.Message.Text = "/help";
                    break;

                case "Back":
                    case "Назад":
                    update.Message.Text = "/back";
                    break;
                case "Daily Weather":
                    case "Погода на день":
                    update.Message.Text = "/dailyweather";
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
