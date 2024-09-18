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

		private ReturnSettingsService _settingsService;
		public SettingsCommand(ReturnSettingsService returnSettingsService)
		{
			_settingsService = returnSettingsService;
		}

		public async Task Execute(Update update)
		{
			long chatId = update.Message.Chat.Id;

			var result = _settingsService.ReturnSettings(update);

			await Client.SendMessageAsync(chatId, result);
		}
	}
}