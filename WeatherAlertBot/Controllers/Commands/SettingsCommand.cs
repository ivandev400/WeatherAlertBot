using Telegram.Bot;
using WeatherAlertBot.Models;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Controllers.Commands
{
	public class StartCommand : ICommand
	{
		public TelegramBotClient Client => Bot.GetTelegramBot();
		public string CommandName => "/settings";
		public string CommandDescription => null;

		public async Task Execute(Update update)
		{
			long chatId = update.Message.Chat.Id;
			
		}
	}
}