using System.Text.Json;

namespace AlfaPay_Admin.Model
{
    public static class Deserializer
    {
        public static ApiResponse<T> DeserializeApiResponse<T>(string response)
        {
            var answer = JsonSerializer.Deserialize<ApiAnswer>(response);
            if (answer.Error != null)
            {
                var error = JsonSerializer.Deserialize<ApiError>(answer.Error.ToString());
                return new ApiResponse<T>(default, error);
            }

            var r = JsonSerializer.Deserialize<T>(answer.Response.ToString());
            return new ApiResponse<T>(r, null);
        }
    }
}