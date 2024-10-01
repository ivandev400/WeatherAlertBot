using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class DailyWeather
    {
        [JsonProperty("temperature_2m_max")]
        public List<double> MaxTemperature { get; set; }

        [JsonProperty("temperature_2m_min")]
        public List<double> MinTemperature { get; set; }

        [JsonProperty("rain_sum")]
        public List<double> RainSum { get; set; }

        [JsonProperty("wind_speed_10m_max")]
        public List<double> MaxWindSpeed { get; set; }
    }
}
