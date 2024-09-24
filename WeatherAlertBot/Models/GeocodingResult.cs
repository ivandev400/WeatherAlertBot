using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class GeocodingResult
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
