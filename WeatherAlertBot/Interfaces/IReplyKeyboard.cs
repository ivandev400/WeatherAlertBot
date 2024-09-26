using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherAlertBot.Interfaces
{
    public interface IReplyKeyboard
    {
        public ReplyKeyboardMarkup GetOneTimeMarkup();
        public ReplyKeyboardMarkup GetPermanentMarkup();
    }
}
