using System.Runtime.CompilerServices;
using Telegram.Bot;

namespace WeatherAlertBot.Models
{
    public class Bot
    {
        public static TelegramBotClient Client { get; private set; }
        private static string BotToken { get; set; }
        public static string GeocodingApiKey { get; set; }

        static Bot()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            BotToken = configuration["BotToken"];
            GeocodingApiKey = configuration["GeocodingApiKey"];
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
