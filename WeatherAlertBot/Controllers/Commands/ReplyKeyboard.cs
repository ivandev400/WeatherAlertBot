using Telegram.Bot.Types.ReplyMarkups;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Controllers.Commands
{
    public class ReplyKeyboard : IReplyKeyboard
    {
        public ReplyKeyboardMarkup GetOneTimeMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {"/changelocation", "/changemorningtime", "/currentweather"}
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetPermanentMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] {"/currentweather", "/settings", "/help"}
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false
            };
        }
    }
}
