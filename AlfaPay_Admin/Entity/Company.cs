using Newtonsoft.Json;

namespace AlfaPay_Admin.Entity
{
    public class Company
    {

        [JsonProperty("companyName")] public string Name { get; set; }

        [JsonProperty("address")] public string Address { get; set; }
        
        [JsonProperty("taxSystem")] public string TaxSystem { get; set; }

        [JsonProperty("inn")] public string Inn { get; set; }
        
        [JsonProperty("kkt")] public string Kkt { get; set; }
    }
    
}