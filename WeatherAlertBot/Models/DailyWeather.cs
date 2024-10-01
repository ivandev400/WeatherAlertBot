using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class DailyWeather
    {
        [JsonProperty("temperature_2m_max")]
        public double MaxTemperature { get; set; }

        [JsonProperty("temperature_2m_min")]
        public double MinTemperature { get; set; }

        [JsonProperty("rain_sum")]
        public double RainSum { get; set; }

        [JsonProperty("wind_speed_10m_max")]
        public double MaxWindSpeed { get; set; }
    }
}
