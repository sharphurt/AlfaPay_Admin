using System.Text.Json.Serialization;

namespace AlfaPay_Admin.Model
{
    public class ApiError
    {
        [JsonPropertyName("message")]
        public string Message { get; }

        [JsonPropertyName("errorCode")]
        public int Code { get; }
        
        [JsonPropertyName("httpCode")]
        public string HttpCode { get; }

        public ApiError(string message, int code, string httpCode)
        {
            Message = message;
            Code = code;
            HttpCode = httpCode;
        }
    }
}