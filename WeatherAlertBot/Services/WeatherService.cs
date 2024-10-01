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
        public string DailyWeatherLink(UserSettings settings, string geocodingApiKey)
        {
            var geocodingResult = LocationToGeocidingResult(settings, geocodingApiKey);
            string link = $"https://api.open-meteo.com/v1/forecast?latitude={geocodingResult.Result.Latitude}&longitude={geocodingResult.Result.Longitude}&hourly=temperature_2m,rain,wind_speed_10m&daily=temperature_2m_max,temperature_2m_min,rain_sum,wind_speed_10m_max&timezone=auto&forecast_days=1";
            return link;
        }
        public async Task<GeocodingResult> LocationToGeocidingResult(UserSettings settings, string geocodingApiKey)
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
        public async Task<WeatherResult> GetCurrentWeatherDataResponse(UserSettings settings, string geocodingApiKey)
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
                        weatherResult.TimeZone = currentWeather.TimeZone;
                    }
                }
                return weatherResult;
            }
        }
        public async Task<DailyWeather> GetDailyWeatherDataResponse(UserSettings settings, string geocodingApiKey)
        {
            var dailyWeather = new DailyWeather();

            using (HttpClient client = new HttpClient())
            {
                string url = DailyWeatherLink(settings, geocodingApiKey);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var currentWeather = JsonConvert.DeserializeObject<DailyWeatherResponse>(responseData);

                    if (currentWeather != null)
                    {
                        dailyWeather = currentWeather.DailyWeather;
                    }
                }
                return dailyWeather;
            }
        }
    }
}
