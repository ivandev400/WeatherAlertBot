using Supabase.Postgrest.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherAlertBot.Models
{
    public class User
    {
        [Key]
        public long ChatId { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}
