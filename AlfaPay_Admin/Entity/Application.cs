using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AlfaPay_Admin.Entity
{
    public class Application
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("phone")] public string Phone { get; set; }

        [JsonPropertyName("email")] public string Email { get; set; }

        [JsonPropertyName("inn")] public string Inn { get; set; }

        [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }     
        
        [JsonPropertyName("status")] public string Status { get; set; }

        public string DaysPassed
        {
            get
            {
                var daysPassed = (DateTime.Now - CreatedAt).Days;
                return daysPassed switch
                {
                    0 => "сегодня",
                    1 => "вчера",
                    2 => "позавчера",
                    _ => $"{daysPassed} {PluralizeDays(daysPassed)} назад"
                };
            }
        }

        private string PluralizeDays(int count)
        {
            if (count % 10 == 1 && count % 100 / 10 != 1) return "день";
            if (count % 10 >= 2 && count % 10 <= 4 && count % 100 / 10 != 1) return "дня";
            return "дней";
        }
    }
}