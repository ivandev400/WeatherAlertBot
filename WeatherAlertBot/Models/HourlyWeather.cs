using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class HourlyWeather
    {
        [JsonProperty("time")]
        public List<DateTime> Hours { get; set; }

        [JsonProperty("temperature_2m")]
        public List<double> Temperature { get; set; }

        [JsonProperty("wind_speed_10m")]
        public List<double> WindSpeed { get; set; }
    }
}
