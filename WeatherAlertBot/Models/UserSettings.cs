using System.ComponentModel.DataAnnotations;

namespace WeatherAlertBot.Models
{
    public class UserSettings
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Location { get; set; }
        public string UpdateInterval { get; set; }
        public TimeOnly MorningTime { get; set; }
        public string TimeZone { get; set; }

        public User User { get; set; }
    }
}
