using Newtonsoft.Json;

namespace AlfaPay_Admin.Model
{
    public class LoggedUser
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        public string Surname { get; set; }
        
        public override string ToString() => $"{FirstName} {Surname}";
    }
}