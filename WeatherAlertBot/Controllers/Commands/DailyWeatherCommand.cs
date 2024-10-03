using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;
using WeatherAlertBot.Services;
using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.AxisLimitManagers;
using System.Reflection.Emit;
using ScottPlot.Plottables;

namespace WeatherAlertBot.Controllers.Commands
{
    public class DailyWeatherCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();
        public string CommandName => "/dailyweather";
        public string CommandDescription { get; set; }

        private WeatherService weatherService => new WeatherService();
        private string geocodingApiKey => Bot.GeocodingApiKey;
        public IGetUserService getUserService;
        public IReturnSettingsService settingsService;
        public IReplyKeyboard replyMarkup;

        public DailyWeatherCommand(IReturnSettingsService settingsService, IReplyKeyboard replyMarkup, IGetUserService getUserService)
        { 
            this.settingsService = settingsService;
            this.replyMarkup = replyMarkup;
            this.getUserService = getUserService;
        }

        public string? Recommendation = null;

        public async Task Execute(Update update)
        {
            long chatId = update.Message.Chat.Id;
            var userSettings = settingsService.ReturnSettings(update);
            var weatherResult = await weatherService.GetDailyWeatherDataResponse(userSettings, geocodingApiKey);

            var dailyWeather = weatherResult.DailyWeather;
            var hourlyWeather = weatherResult.HourlyWeather;

            var morningData = FilterTimeRange(hourlyWeather, 5, 11);

            var rainProbabilityText = userSettings.Language == "en" ? "Rain Probability: " : "Вірогідність опадів: ";

            var textMessage = $"MAX: {dailyWeather.MaxTemperature.FirstOrDefault()}°C \n" +
                $"MIN: {dailyWeather.MinTemperature.FirstOrDefault()}°C \n" +
                $"{rainProbabilityText}{dailyWeather.RainSum.FirstOrDefault()} \n" +
                $"MAX: {dailyWeather.MaxWindSpeed.FirstOrDefault()}km/h \n\n\n\n" +

                $"Average temperature morning: {Math.Round(morningData.Average(x => x.temperature), 1)}°C \n" +
                $"Avarage rain probability: {Math.Round(morningData.Average(x => x.rain), 1)}mm \n" +
                $"Average wind speed: {Math.Round(morningData.Average(x => x.windSpeed), 1)}km/h";

            await Client.SendTextMessageAsync(chatId, textMessage, replyMarkup: replyMarkup.GetPermanentMarkup(userSettings.Language));
            await GenerateWeatherPlot(hourlyWeather, update, userSettings.Language);
            Recommendation = null;
        }

        public async Task SetDescription(Update update)
        {
            CommandDescription = null;
        }
        private IEnumerable<(double temperature, double rain, double windSpeed)> FilterTimeRange(HourlyWeather hourly, int startHour, int endHour)
        {
            var filteredData = hourly.Hours
                .Where(h => h.Hour >= startHour && h.Hour <= endHour)
                .Select(h => (
                temperature: hourly.Temperature[hourly.Hours.IndexOf(h)],
                rain: hourly.Rain[hourly.Hours.IndexOf(h)],
                windSpeed: hourly.WindSpeed[hourly.Hours.IndexOf(h)]
            ));

            return filteredData;
        }

        public async Task GenerateWeatherPlot(HourlyWeather hourlyWeather, Update update, string language)
        {
            var plot = new Plot();

            List<int> dataX = hourlyWeather.Hours.Select(h => h.Hour).ToList();
            List<double> temperatureDataY = hourlyWeather.Temperature;
            List<double> windSpeedDataY = hourlyWeather.WindSpeed;

            double[] doubleX = dataX.Select(x => (double)x).ToArray();

            var windSpeedScatter = plot.Add.Scatter(dataX, windSpeedDataY, color: ScottPlot.Color.FromColor(System.Drawing.Color.Green));
            var tempScatter = plot.Add.Scatter(dataX, temperatureDataY, color: ScottPlot.Color.FromColor(System.Drawing.Color.Red));

            for (int i = 0; i < hourlyWeather.Rain.Count; i++)
            {
                if (hourlyWeather.Rain[i] > 0)
                {
                    var bar = new Bar();
                    bar.Value = hourlyWeather.Rain[i];
                    bar.Position = dataX[i];
                    bar.FillColor = ScottPlot.Color.FromColor(System.Drawing.Color.DarkBlue);
                    bar.BorderLineWidth = 0;  
                    
                    if (i == 0) { bar.Label = language == "en" ? "Rain mm" : "Опади mm"; }

                    plot.Add.Bar(bar);
                }
            }

            tempScatter.LegendText = language == "en" ? "Temperature °C" : "Температура °C";
            windSpeedScatter.LegendText = language == "en" ? "Wind Speed km/h" : "Швидкість вітру km/h";

            plot.Title(language == "en" ? "Daily weather" : "Погода на день");
            plot.XLabel(language == "en" ? "Hours" : "Години");
            plot.YLabel(language == "en" ? "Values" : "Значення");

            var filePath = "demo.png";
            plot.SavePng(filePath, 800, 400);

            using (var stream = System.IO.File.OpenRead(filePath))
            {
                await Client.SendPhotoAsync(update.Message.Chat.Id, new InputFileStream(stream));

                System.IO.File.Delete(filePath);
            }
        }
    }
}
