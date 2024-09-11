using Microsoft.AspNetCore.Components.Forms;

namespace WeatherAlertBot.Models
{
    public class UserSettings
    {
        public long UserID { get; set; }
        public string Location { get; set; }
        public string UpdateInterval { get; set; }
        public TimeOnly MorningTime { get; set; }
    }
}
