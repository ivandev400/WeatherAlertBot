using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class ImageData
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
