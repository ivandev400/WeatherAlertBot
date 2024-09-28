using Telegram.Bot.Types;
using Telegram.Bot;
using WeatherAlertBot.Models;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;
using Supabase.Gotrue;

namespace WeatherAlertBot.Controllers.Commands
{
    public class CurrentWeatherCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/currentweather";
        public string CommandDescription => CommandDescriptions.CurrentWeather;

        private WeatherService weatherService => new WeatherService();
        private string geocodingApiKey => Bot.GeocodingApiKey;
        public IReturnSettingsService settingsService;
        public IReplyKeyboard replyMarkup;

        public CurrentWeatherCommand(IReturnSettingsService settingsService, IReplyKeyboard replyMarkup)
        {
            this.settingsService = settingsService;
            this.replyMarkup = replyMarkup;
        }

        public string? Recommendation = null; 

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var userSettings = settingsService.ReturnSettings(update);
            var weatherResult = await weatherService.GetWeatherDataStringResponse(userSettings, geocodingApiKey);

            string message = $"⌚   {weatherResult.Time.ToString("HH:mm")}\n" +
                             $"🌡️   {weatherResult.Temperature}°C   {TemeperatureConverter(weatherResult.Temperature)}\n" +
                             $"🌩️   {weatherResult.Rain}   {RainConverter(weatherResult.Rain)}\n" +
                             $"🍃   {weatherResult.WindSpeed} km/h\r\n\r\n" +
            Recommendation;

            await Client.SendTextMessageAsync(chatId, message, replyMarkup: replyMarkup.GetPermanentMarkup(userSettings.Language));
            Recommendation = null;
        }
        private string RainConverter(double rain)
        {
            switch (rain)
            {
                case <= 0.5:
                    Recommendation += "Можна не боятися намокнути ©️ \n";
                    return "Дощу нема 🌤️";
                case > 0.5 and <= 2:
                    Recommendation += "Щось таки треба вдягнути ©️ \n";
                    return "Моросить 💧";
                case > 2 and <= 6:
                    Recommendation += "Тут точно треба парасоля ©️ \n";
                    return "Середній дощ ☔";
                case > 6 and <= 10:
                    Recommendation += "Треба бути обережним, тут парасоля мало чим допоможе ©️ \n";
                    return "Сильний дощ 🌧️";
                case > 10 and <= 18:
                    Recommendation += "Без коментарів, рекомендую залишитись вдома ©️ \n";
                    return "Дуже сильний дощ 😶‍🌫";
                case > 18:
                    Recommendation += "Ну тут без коментарів, це смерть... ©️ \n";
                    return "Л'є як із відра, найсильніший дощ ⚠️⚠️";
            }
            return "";
        }
        private string TemeperatureConverter(double temperature)
        {
            switch (temperature)
            {
                case < -30:
                    Recommendation += "Тут без обладунків Антарктики ніяк ©️\n";
                    return "Без коментарів 🥶";
                case < -15 and >= -30:
                    Recommendation += "Це косплей на крижане серце ©️\n";
                    return "Дуже холодно 🧊";
                case < 0 and >= -15:
                    Recommendation += "Рекомендую вдягнути щось тепленьке ©️\n";
                    return "Холодно ❄️";
                case > 0 and <= 10:
                    Recommendation += "Весняна погодка, рекомендую щось вдягнути ©️\n";
                    return "З вітерцем ༄";
                case > 10 and <= 20:
                    Recommendation += "Золота серединка по температурі ©️\n";
                    return "Тепленько 🔅";
                case > 20 and <= 30:
                    Recommendation += "Жарко, з одягом можна не паритись ©️\n";
                    return "Жара 🔥";
                case > 30 and <= 45:
                    Recommendation += "Бажаю удачі ©️\n";
                    return "Пекло ⁶⁶⁶";
                case > 45:
                    Recommendation += "Ти приймаєш участь в голодних іграх, гра почалась... ©️";
                    return "Це десь ядро Землі 🌋";
            }
            return "";
        }
    }
}
