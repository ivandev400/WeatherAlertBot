using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Controllers.Commands
{
    public class BackCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/back";
        public string CommandDescription { get; set; }

        public IGetUserService getUserService;
        public IReplyKeyboard replyMarkup;


        public BackCommand(IGetUserService getUserService, IReplyKeyboard replyMarkup)
        {
            this.getUserService = getUserService;
            this.replyMarkup = replyMarkup;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var user = getUserService.GetUser(update);

            await Client.SendTextMessageAsync(chatId, ".", replyMarkup: replyMarkup.GetPermanentMarkup(user.Language));
        }

        public async Task SetDescription(Update update)
        {
            CommandDescription = null;
        }
    }
}
