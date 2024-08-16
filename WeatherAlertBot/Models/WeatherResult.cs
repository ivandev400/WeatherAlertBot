namespace WeatherAlertBot.Models
{
    public class WeatherResult
    {
        public DateTime Time {  get; set; }
        public double Temperature { get; set; }
        public double Rain { get; set; }
        public double WindSpeed { get; set; }
    }
}
