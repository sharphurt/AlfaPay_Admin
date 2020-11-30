using System.Text.Json.Serialization;

namespace AlfaPay_Admin.Model
{
    public class ApiAnswer
    {
        [JsonPropertyName("response")]
        public object Response { get; set; }
        
        [JsonPropertyName("error")]
        public object Error { get; set; }
        
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
    }
}