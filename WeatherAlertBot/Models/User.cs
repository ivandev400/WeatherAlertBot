using System.ComponentModel.DataAnnotations;

namespace WeatherAlertBot.Models
{
    public class User
    {
        [Key]
        public long ChatId { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}
