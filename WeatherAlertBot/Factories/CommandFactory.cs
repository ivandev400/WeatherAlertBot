using WeatherAlertBot.Interfaces;
using Telegram.Bot;
using WeatherAlertBot.Controllers.Commands;
using WeatherAlertBot.Services;

namespace WeatherAlertBot.Factories
{
    public class CommandFactory
    {
        public static T CreateCommand<T> (IServiceProvider serviceProvider) where T : ICommand
        {
            var command = ActivatorUtilities.CreateInstance<T>(serviceProvider);

            switch (command)
            {
                case SettingsCommand settingsCommand:
                    settingsCommand.SettingsService = serviceProvider.GetRequiredService<ReturnSettingsService>();
                    break;
            }

            return command;
        }
    }
}
