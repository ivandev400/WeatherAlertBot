using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Controllers
{
    public class UpdateDistributor<T> where T : ITelegramUpdateListener
    {
        private Dictionary<long, T> listeners;
        private IServiceProvider serviceProvider;

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
                listener = serviceProvider.GetRequiredService<T>();
                listeners.Add(chatId, listener);
            }

            await listener.GetUpdate(update);
        }
    }
}
