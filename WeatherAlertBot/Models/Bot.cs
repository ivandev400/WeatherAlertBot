using System.Runtime.CompilerServices;
using Telegram.Bot;

namespace WeatherAlertBot.Models
{
    public class Bot
    {
        public static TelegramBotClient Client { get; private set; }
        private static string BotToken { get; set; }

        public Bot(IConfiguration configuration)
        {
            BotToken = configuration["BotToken"];
        }

        public static TelegramBotClient GetTelegramBot()
        {
            if (Client != null)
            {
                return Client;
            }
            Client = new TelegramBotClient(BotToken);
            return Client;
        }
    }
}
