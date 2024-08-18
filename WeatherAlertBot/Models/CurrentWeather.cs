using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class CurrentWeather
    {
        [JsonProperty("current")]
        public WeatherResult Current {  get; set; }

        public CurrentWeather()
        {
            Current = new WeatherResult();
        }
    }
}
