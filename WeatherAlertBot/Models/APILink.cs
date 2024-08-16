using GoogleMaps.LocationServices;

namespace WeatherAlertBot.Models
{
    public class APILink
    {
        private double Latitude {  get; set; }
        private double Longitude { get; set; }

        public string UpdatedAPILink(UserSettings settings)
        {
            string link = $"https://api.open-meteo.com/v1/forecast?latitude={Latitude}&longitude={Longitude}&current=temperature_2m,rain,wind_speed_10m&forecast_days=1";
            return link;
        }
        private void LocationConvertor(UserSettings settings)
        {
            var address = settings.Location;

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            Latitude = point.Latitude;
            Longitude = point.Longitude;
        }
        public async Task<string> GetWeatherDataStringResponse(UserSettings settings)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = UpdatedAPILink(settings);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
        }
    }
}
