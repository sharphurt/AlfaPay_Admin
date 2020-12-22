using System;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using AlfaPay_Admin.Converter;
using AlfaPay_Admin.Enum;
using Newtonsoft.Json.Converters;

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

        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ApplicationStatus Status { get; set; }

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

        public bool MatchToSearchString(string searchString) =>
            string.IsNullOrEmpty(searchString) ||
            searchString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).All(Match);

        public bool MatchToFilter(ApplicationFilter filter)
        {
            return Status == ApplicationStatus.NotConsidered && filter.IncludeNew
                   || Status == ApplicationStatus.Accepted && filter.IncludeAccepted
                   || Status == ApplicationStatus.Rejected && filter.IncludeRejected;
        }


        private bool Match(string search)
        {
            return Email.ToLower().Contains(search)
                   || Inn.ToLower().Contains(search)
                   || Name.ToLower().Contains(search)
                   || Phone.ToLower().Contains(search)
                   || Status.ToString().ToLower().Contains(search);
        }
    }
}