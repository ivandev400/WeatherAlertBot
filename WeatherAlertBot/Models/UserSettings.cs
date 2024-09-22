using System.ComponentModel.DataAnnotations;

namespace WeatherAlertBot.Models
{
    public class UserSettings
    {
        [Key]
        public long UserID { get; set; }
        public string Location { get; set; }
        public string UpdateInterval { get; set; }
        public TimeOnly MorningTime { get; set; }

        public User User { get; set; }
    }
}
