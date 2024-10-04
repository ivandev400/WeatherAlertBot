using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Interfaces
{
    public interface IWeatherPlotService
    {
        public Task GenerateWeatherPlot(TelegramBotClient Client, HourlyWeather hourlyWeather, Update update, string language);
    }
}
