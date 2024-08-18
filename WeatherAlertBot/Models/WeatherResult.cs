using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class WeatherResult
    {
        [JsonProperty("time")]
        public DateTime Time {  get; set; }

        [JsonProperty("temperature_2m")]
        public double Temperature { get; set; }

        [JsonProperty("rain")]
        public double Rain { get; set; }

        [JsonProperty("wind_speed_10m")]
        public double WindSpeed { get; set; }
    }
}
