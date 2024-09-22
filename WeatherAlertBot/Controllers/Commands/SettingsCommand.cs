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

		public IReturnSettingsService settingsService;

		public SettingsCommand(IReturnSettingsService settingsService)
		{
			this.settingsService = settingsService;
		}

		public async Task Execute(Update update)
		{
			long chatId = update.Message.Chat.Id;
            settingsService.ReturnSettings(update);

			await Client.SendTextMessageAsync(chatId, CommandDescription);
		}
	}
}