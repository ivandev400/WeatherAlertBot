namespace WeatherAlertBot.Interfaces
{
    public interface IImageGeneratorService
    {
        public Task<string> GenerateImageAsync(string chatGPTApiKey, string prompt);
    }
}
