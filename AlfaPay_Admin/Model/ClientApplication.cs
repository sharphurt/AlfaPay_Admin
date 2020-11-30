﻿using System.Text.Json.Serialization;

 namespace AlfaPay_Admin.Model
{
    public class ClientApplication
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("inn")]
        public long Inn { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{Name} \t {Phone}";
        }
    }
}