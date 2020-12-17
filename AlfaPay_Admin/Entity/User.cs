using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AlfaPay_Admin.Entity
{
    public class User
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("firstNname")] public string FirstName { get; set; }
        
        [JsonPropertyName("lastNname")] public string LastName { get; set; }

        [JsonPropertyName("phone")] public string Phone { get; set; }

        [JsonPropertyName("email")] public string Email { get; set; }
        
    }
}