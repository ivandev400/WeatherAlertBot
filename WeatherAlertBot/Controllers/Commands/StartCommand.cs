using Telegram.Bot;
using WeatherAlertBot.Models;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
    public class StartCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/start";
        public string CommandDescription => CommandDescriptions.StartCommand;
        public ICreateUserService createUserService;

        public StartCommand(ICreateUserService createUserService)
        {
            this.createUserService = createUserService;
        }

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            createUserService.CreateUser(update);
            string textMessage = "This is Weather Alert Bot(or storm watch).This Bot was created to help you to know weather at time. Here you can use different commands shown below - immidiately send you message with weather info in location that you can see and set using command /settings (default location is Kyiv).\r\n\r\nЭто Weather Alert Bot (или Storm Watch). Этот бот был создан, чтобы помочь вам узнать погоду в данный момент в любой точке мира и другие плюшки. Здесь вы можете использовать различные команды показаны ниже.Бот может немедленно отправить вам сообщение с информацией о погоде в любой точке мира, которое вы можете посмотреть и указать с помощью команды /settings (местоположение по умолчанию - Киев).";
            await Client.SendTextMessageAsync(chatId, textMessage);
        }
    }
}
