using ScottPlot;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherAlertBot.Models;
using WeatherAlertBot.Interfaces;

namespace WeatherAlertBot.Services
{
    public class WeatherPlotService : IWeatherPlotService
    {
        public async Task GenerateWeatherPlot(TelegramBotClient Client, HourlyWeather hourlyWeather, Update update, string language)
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
