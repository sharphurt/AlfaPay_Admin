using System;
using System.Text.Json.Serialization;
using AlfaPay_Admin.Annotations;

namespace AlfaPay_Admin.Model
{
    public class ClientApplication
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("phone")] public string Phone { get; set; }

        [JsonPropertyName("email")] public string Email { get; set; }

        [JsonPropertyName("inn")] public long Inn { get; set; }

        [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }

        public string DaysPassed
        {
            get
            {
                var daysPassed = (DateTime.Now - CreatedAt).Days;
                return $"{daysPassed} {PluralizeRubles(daysPassed)} назад";
            }
        }

        private string PluralizeRubles(int count)
        {
            var firstRightDigit = count % 10;
            var secondRightDigits = count % 100 / 10;
            if (firstRightDigit == 1 && secondRightDigits != 1)
                return "день";
            if (firstRightDigit >= 2
                && firstRightDigit <= 4
                && secondRightDigits != 1)
                return "дня";
            return "дней";
        }

        public override string ToString()
        {
            return $"{Name} \t {Phone}";
        }
    }
}