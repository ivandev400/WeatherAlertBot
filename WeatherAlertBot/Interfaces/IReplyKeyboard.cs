using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherAlertBot.Interfaces
{
    public interface IReplyKeyboard
    {
        public ReplyKeyboardMarkup GetOneTimeMarkup(string language);
        public ReplyKeyboardMarkup GetPermanentMarkup(string language);
        public ReplyKeyboardMarkup GetBoolMarkup();
        public ReplyKeyboardMarkup GetLanguageMarkup();
    }
}
