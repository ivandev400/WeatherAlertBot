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

		public IReturnSettingsService _returnSettings;

		public SettingsCommand(IReturnSettingsService returnSettings)
		{
			_returnSettings = returnSettings;
		}

		public async Task Execute(Update update)
		{
			long chatId = update.Message.Chat.Id;
            var result = _returnSettings.ReturnSettings(update);

			await Client.SendTextMessageAsync(chatId, result);
		}
	}
}