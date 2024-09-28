using Supabase.Gotrue;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
    public class DailyWeatherCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/dailyweather";
        public string CommandDescription => CommandDescriptions.CurrentWeather;

        private WeatherService weatherService => new WeatherService();
        private string geocodingApiKey => Bot.GeocodingApiKey;
        public IGetUserService getUserService;
        public IReturnSettingsService settingsService;
        public IReplyKeyboard replyMarkup;

        public DailyWeatherCommand(IReturnSettingsService settingsService, IReplyKeyboard replyMarkup, IGetUserService getUserService)
        { 
            this.settingsService = settingsService;
            this.replyMarkup = replyMarkup;
            this.getUserService = getUserService;
        }

        public string? Recommendation = null;

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var userSettings = settingsService.ReturnSettings(update);
            var weatherResult = await weatherService.GetWeatherDataStringResponse(userSettings, geocodingApiKey);

            await Client.SendTextMessageAsync(chatId, "This is daily predication", replyMarkup: replyMarkup.GetPermanentMarkup(userSettings.Language));
            Recommendation = null;
        }
    }
}
