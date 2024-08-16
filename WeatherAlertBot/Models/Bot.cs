using Telegram.Bot;

namespace WeatherAlertBot.Models
{
    public class Bot
    {
        public static TelegramBotClient client { get; private set; }
        
        public static TelegramBotClient GetTelegramBot(string botToken)
        {
            if(client != null)
            {
                return client;
            }
            client = new TelegramBotClient(botToken);
            return client;
        }
    }
}
