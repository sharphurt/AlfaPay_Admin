using Newtonsoft.Json;

namespace AlfaPay_Admin.Entity
{
    public class Registration
    {
        [JsonProperty("company")] public Company Company { get; set; }
    }
}