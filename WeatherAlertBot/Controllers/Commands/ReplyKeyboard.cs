using Telegram.Bot.Types.ReplyMarkups;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Controllers.Commands
{
    public class ReplyKeyboard : IReplyKeyboard
    {
        public ReplyKeyboardMarkup GetOneTimeMarkup(string language)
        {
            string changeLocationText = language == "en" ? "Change Location" : "Змінити місце";
            string changeMorningTimeText = language == "en" ? "Change notification morning time" : "Змінити час ранкового сповіщення";
            string backText = language == "en" ? "Back" : "Назад";
            string anableMorningNotificationText = language == "en" ? "Anable Morning Notifications" : "Дозволити Ранкові Сповіщення";

            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { changeLocationText },
                new KeyboardButton[] { changeMorningTimeText },
                new KeyboardButton[] { anableMorningNotificationText },
                new KeyboardButton[] { backText }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetPermanentMarkup(string language)
        {
            string currentWeatherText = language == "en" ? "Current Weather" : "Поточна погода";
            string dailyWeatherText = language == "en" ? "Daily Weather" : "Погода на день";
            string settingsText = language == "en" ? "Settings" : "Налаштування";
            string setLanguageText = language == "en" ? "Language" : "Мова";
            string helpText = language == "en" ? "Help" : "Допомогти";

            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { currentWeatherText },
                new KeyboardButton[] { dailyWeatherText },
                new KeyboardButton[] { settingsText },
                new KeyboardButton[] { setLanguageText },
                new KeyboardButton[] { helpText }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false
            };
        }

        public ReplyKeyboardMarkup GetBoolMarkup(string language)
        {
            string yesText = language == "en" ? "YES" : "ТАК";
            string noText = language == "en" ? "NO" : "НІ";

            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { yesText },
                new KeyboardButton[] { noText },
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false
            };
        }

        public ReplyKeyboardMarkup GetLanguageMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "🇬🇧EN🇬🇧" },
                new KeyboardButton[] { "🇺🇦UA🇺🇦" },
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false
            };
        }
    }
}
