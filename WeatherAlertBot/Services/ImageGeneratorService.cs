using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WeatherAlertBot.Interfaces;
using WeatherAlertBot.Models;

namespace WeatherAlertBot.Services
{
    public class ImageGeneratorService : IImageGeneratorService
    {
        public async Task<string> GenerateImageAsync(string chatGPTApiKey, string prompt)
        {
            string imageLink = "";

            var resuestBody = new
            {
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Barear",  chatGPTApiKey);

                var requestContent = new StringContent(JsonConvert.SerializeObject(resuestBody), Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://api.openai.com/v1/images/generations", requestContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<ImageModelResponse>(responseContent);

                    imageLink = responseData.ImageData[0].Url.ToString();
                }
            }

            return imageLink;
        }
    }
}
