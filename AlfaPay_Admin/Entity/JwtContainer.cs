using Newtonsoft.Json;

namespace AlfaPay_Admin.Entity
{
    public class JwtContainer
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        public override string ToString() => $"{TokenType} {Token}";
    }
}