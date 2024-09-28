using System.ComponentModel.DataAnnotations;

namespace WeatherAlertBot.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public long UserSettingsId { get; set; }
        public long ChatId { get; set; }
        public string Language { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}
