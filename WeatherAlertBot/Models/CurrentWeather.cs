using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class CurrentWeather
    {
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("current")]
        public WeatherResult Current {  get; set; }

        public CurrentWeather()
        {
            Current = new WeatherResult();
        }
    }
}
