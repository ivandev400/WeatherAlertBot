using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class DailyWeatherResponse
    {
        [JsonProperty("hourly")]
        public HourlyWeather HourlyWeather { get; set; }

        [JsonProperty("daily")]
        public DailyWeather DailyWeather { get; set; }

        public DailyWeatherResponse()
        {
            HourlyWeather = new HourlyWeather();
            DailyWeather = new DailyWeather();
        }
    }
}
