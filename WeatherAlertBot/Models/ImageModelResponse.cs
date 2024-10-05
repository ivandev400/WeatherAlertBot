using Newtonsoft.Json;

namespace WeatherAlertBot.Models
{
    public class ImageModelResponse
    {
        [JsonProperty("data")]
        public List<ImageData> ImageData { get; set; }

        public ImageModelResponse()
        {
            ImageData = new List<ImageData>();
        }
    }
}
