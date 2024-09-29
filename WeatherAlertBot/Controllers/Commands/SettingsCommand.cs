using Telegram.Bot;
using WeatherAlertBot.Models;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using Telegram.Bot.Types.Enums;
using Supabase.Gotrue;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Controllers.Commands
{
	public class SettingsCommand : ICommand
	{
		public TelegramBotClient Client => Bot.GetTelegramBot();
		public string CommandName => "/settings";
		public string CommandDescription {  get; set; }

		public IReturnSettingsService settingsService;
		public IReplyKeyboard replyMarkup;


        public SettingsCommand(IReturnSettingsService settingsService, IReplyKeyboard replyMarkup)
		{
			this.settingsService = settingsService;
			this.replyMarkup = replyMarkup;
		}

		public async Task Execute(Update update)
		{
			long chatId = update.Message.Chat.Id;
            string stringSettings = settingsService.ReturnSettingsToString(update);

			var settings = settingsService.ReturnSettings(update);

            await Client.SendTextMessageAsync(chatId, stringSettings, null, ParseMode.Html, replyMarkup: replyMarkup.GetOneTimeMarkup(settings.Language));
		}

        public async Task SetDescription(Update update)
        {
            var settings = settingsService.ReturnSettings(update);
            CommandDescription = settings.Language == "en" ? CommandDescriptions.SettingsEN : CommandDescriptions.SettingsUA;
        }
    }
}