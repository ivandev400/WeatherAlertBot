using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Controllers
{
    public class UpdateDistributor<T> where T : ITelegramUpdateListener
    {
        private Dictionary<long, T> listeners;
        public IServiceProvider serviceProvider;

        public UpdateDistributor(IServiceProvider serviceProvider)
        {
            listeners = new Dictionary<long, T>();
            this.serviceProvider = serviceProvider; 
        }

        public async Task GetUpdate(Update update)
        {
            long chatId = update.Message.Chat.Id;

            if (!listeners.TryGetValue(chatId, out T? listener))
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    listener = scope.ServiceProvider.GetService<T>();

                    if (listener == null)
                    {
                        throw new InvalidOperationException($"Unable to resolve listener of type {typeof(T).Name}");
                    }

                    listeners.Add(chatId, listener);
                }
            }
            await listener.GetUpdate(update);
        }
    }
}
