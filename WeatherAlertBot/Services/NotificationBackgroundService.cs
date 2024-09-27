using Telegram.Bot;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;

public class NotificationBackgroundService : IHostedService, IDisposable
{
    private IDailyNotifier _dailyNotifier;
    private Timer _notificationTimer;

    public NotificationBackgroundService(IDailyNotifier _dailyNotifier)
    {
        this._dailyNotifier = _dailyNotifier;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _notificationTimer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        Task.Run(() => _dailyNotifier.SendDailyNotification());
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _notificationTimer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _notificationTimer?.Dispose();
    }
}
