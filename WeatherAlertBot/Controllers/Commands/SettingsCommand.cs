using Telegram.Bot;
using WeatherAlertBot.Models;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
	public class SettingsCommand : ICommand
	{
		public TelegramBotClient Client => Bot.GetTelegramBot();
		public string CommandName => "/settings";
		public string CommandDescription => null;

		public ReturnSettingsService SettingsService { get; set; }

		public async Task Execute(Update update)
		{
			long chatId = update.Message.Chat.Id;
            var result = SettingsService.ReturnSettings(update);

			await Client.SendTextMessageAsync(chatId, result);
		}
	}
}