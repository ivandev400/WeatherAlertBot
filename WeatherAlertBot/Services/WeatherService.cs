using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class WeatherService
    {
        public string CurrentWeatherLink(UserSettings settings, string geocodingApiKey)
        {
            var geocodingResult = LocationToGeocidingResult(settings, geocodingApiKey);
            string link = $"https://api.open-meteo.com/v1/forecast?latitude={geocodingResult.Result.Latitude}&longitude={geocodingResult.Result.Longitude}&current=temperature_2m,rain,wind_speed_10m&timezone=auto&forecast_days=1";
            return link;
        }
        public string DalyWeatherLink(UserSettings settings, string geocodingApiKey)
        {
            var geocodingResult = LocationToGeocidingResult(settings, geocodingApiKey);
            string link = $"https://api.open-meteo.com/v1/forecast?latitude={geocodingResult.Result.Latitude}&longitude={geocodingResult.Result.Longitude}&current=temperature_2m,rain,wind_speed_10m&timezone=auto&forecast_days=1";
            return link;
        }
        private async Task<GeocodingResult> LocationToGeocidingResult(UserSettings settings, string geocodingApiKey)
        {
            var address = settings.Location;
            var geocodingResult = new GeocodingResult();

            using (HttpClient client = new HttpClient())
            {
                string url = $"https://geocode.maps.co/search?q={address}&api_key={geocodingApiKey}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<List<GeocodingResult>>(responseData);

                    if (results != null)
                    {
                        var firstResult = results.FirstOrDefault();
                        geocodingResult = firstResult;
                    }
                }
                return geocodingResult;
            }
        }
        public async Task<WeatherResult> GetWeatherDataStringResponse(UserSettings settings, string geocodingApiKey)
        {
            var weatherResult = new WeatherResult();

            using (HttpClient client = new HttpClient())
            {
                string url = CurrentWeatherLink(settings, geocodingApiKey);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(responseData);

                    if (currentWeather != null)
                    {
                        weatherResult = currentWeather.Current;
                    }
                }
                return weatherResult;
            }
        }
    }
}
