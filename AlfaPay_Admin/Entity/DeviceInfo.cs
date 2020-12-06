using Newtonsoft.Json;

namespace AlfaPay_Admin.Entity
{
    public class DeviceInfo
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
        
        [JsonProperty("deviceType")]
        public string DeviceType { get; set; }
        

        public DeviceInfo(string deviceId)
        {
            DeviceId = deviceId;
            DeviceType = "DEVICE_TYPE_WINDOWS";
        }
    }
}