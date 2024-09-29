using Telegram.Bot.Types;
using Telegram.Bot;
using WeatherAlertBot.Models;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;
using Telegram.Bot.Types.Enums;

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
        public IGetUserService getUserService;

        public CurrentWeatherCommand(IReturnSettingsService settingsService, IReplyKeyboard replyMarkup, IGetUserService getUserService)
        {
            this.settingsService = settingsService;
            this.replyMarkup = replyMarkup;
            this.getUserService = getUserService;
        }

        public string? Recommendation = null;

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);
            var userSettings = settingsService.ReturnSettings(update);
            var weatherResult = await weatherService.GetWeatherDataStringResponse(userSettings, geocodingApiKey);

            string message = $"⌚   <b>{weatherResult.Time.ToString("HH:mm")}</b>\n" +
                             $"🌡️   <b>{weatherResult.Temperature}°C</b> - {TemperatureConverter(weatherResult.Temperature, user.Language)}\n" +
                             $"🌩️   <b>{weatherResult.Rain} mm</b> - {RainConverter(weatherResult.Rain, user.Language)}\n" +
                             $"🍃   <b>{weatherResult.WindSpeed} km/h</b>\r\n\r\n" +
            Recommendation;

            await Client.SendTextMessageAsync(chatId, message, null, ParseMode.Html, replyMarkup: replyMarkup.GetPermanentMarkup(userSettings.Language));
            Recommendation = null;
        }
        private string RainConverter(double rain, string language)
        {
            switch (rain)
            {
                case <= 0.5:
                    Recommendation += language == "en"
                        ? "No need to worry about getting wet 🌈©️ \n"
                        : "Можна не боятися намокнути 🌈©️ \n";
                    return language == "en"
                        ? "No rain 🌤️"
                        : "Дощу нема 🌤️";
                case > 0.5 and <= 2:
                    Recommendation += language == "en"
                        ? "Might want to wear something light 🧥©️ \n"
                        : "Щось таки треба вдягнути 🧥©️ \n";
                    return language == "en"
                        ? "Drizzling 💧"
                        : "Моросить 💧";
                case > 2 and <= 6:
                    Recommendation += language == "en"
                        ? "Definitely bring an umbrella ☂️©️ \n"
                        : "Тут точно треба парасоля ☂️©️ \n";
                    return language == "en"
                        ? "Moderate rain ☔"
                        : "Середній дощ ☔";
                case > 6 and <= 10:
                    Recommendation += language == "en"
                        ? "Be cautious, an umbrella won't help much ⚠️🌧️©️ \n"
                        : "Треба бути обережним, тут парасоля мало чим допоможе ⚠️🌧️©️ \n";
                    return language == "en"
                        ? "Heavy rain 🌧️"
                        : "Сильний дощ 🌧️";
                case > 10 and <= 18:
                    Recommendation += language == "en"
                        ? "Better stay home 🏠©️ \n"
                        : "Без коментарів, рекомендую залишитись вдома 🏠©️ \n";
                    return language == "en"
                        ? "Very heavy rain 😶‍🌫"
                        : "Дуже сильний дощ 😶‍🌫";
                case > 18:
                    Recommendation += language == "en"
                        ? "No comments, this is death... 🌊©️ \n"
                        : "Ну тут без коментарів, це смерть... 🌊©️ \n";
                    return language == "en"
                        ? "Pouring like crazy, the heaviest rain ⚠️⚠️"
                        : "Л'є як із відра, найсильніший дощ ⚠️⚠️";
            }
            return "";
        }

        private string TemperatureConverter(double temperature, string language)
        {
            switch (temperature)
            {
                case < -30:
                    Recommendation += language == "en"
                        ? "You'll need Antarctic gear for this 🧊🐧©️\n"
                        : "Тут без обладунків Антарктики ніяк 🧊🐧©️\n";
                    return language == "en"
                        ? "No comment 🥶"
                        : "Без коментарів 🥶";
                case < -15 and >= -30:
                    Recommendation += language == "en"
                        ? "Feels like a Frozen cosplay 🥶❄️©️\n"
                        : "Це косплей на крижане серце 🥶❄️©️\n";
                    return language == "en"
                        ? "Very cold 🧊"
                        : "Дуже холодно 🧊";
                case < 0 and >= -15:
                    Recommendation += language == "en"
                        ? "I recommend wearing something warm 🧣🧥©️\n"
                        : "Рекомендую вдягнути щось тепленьке 🧣🧥©️\n";
                    return language == "en"
                        ? "Cold ❄️"
                        : "Холодно ❄️";
                case > 0 and <= 10:
                    Recommendation += language == "en"
                        ? "Spring-like weather, wear something light 🌷🧥©️\n"
                        : "Весняна погодка, рекомендую щось вдягнути 🌷🧥©️\n";
                    return language == "en"
                        ? "Breezy ༄"
                        : "З вітерцем ༄";
                case > 10 and <= 20:
                    Recommendation += language == "en"
                        ? "Perfect temperature, just right 🌸☀️©️\n"
                        : "Золота серединка по температурі 🌸☀️©️\n";
                    return language == "en"
                        ? "Warm 🔅"
                        : "Тепленько 🔅";
                case > 20 and <= 30:
                    Recommendation += language == "en"
                        ? "It's hot, no need to overthink the outfit 🌞👕©️\n"
                        : "Жарко, з одягом можна не паритись 🌞👕©️\n";
                    return language == "en"
                        ? "Hot 🔥"
                        : "Жара 🔥";
                case > 30 and <= 45:
                    Recommendation += language == "en"
                        ? "Good luck 🔥☀️©️\n"
                        : "Бажаю удачі 🔥☀️©️\n";
                    return language == "en"
                        ? "Scorching ⁶⁶⁶"
                        : "Пекло ⁶⁶⁶";
                case > 45:
                    Recommendation += language == "en"
                        ? "You're in the Hunger Games now... 🏹©️"
                        : "Ти приймаєш участь в голодних іграх, гра почалась... 🏹©️";
                    return language == "en"
                        ? "This is the Earth's core 🌋"
                        : "Це десь ядро Землі 🌋";
            }
            return "";
        }

    }
}
