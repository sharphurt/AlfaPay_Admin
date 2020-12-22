using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfaPay_Admin.Entity
{
    public class User
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("firstName")] public string FirstName { get; set; }

        [JsonPropertyName("lastName")] public string LastName { get; set; }

        [JsonPropertyName("patronymic")] public string Patronymic { get; set; }

        [JsonPropertyName("phone")] public string Phone { get; set; }

        [JsonPropertyName("email")] public string Email { get; set; }

        [JsonProperty("userStatus")] public string UserStatus { get; set; }

        [JsonProperty("userPrivilege")] public string UserPrivilege { get; set; }

        [JsonProperty("registrations")] public List<Registration> Registrations { get; set; }

        [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }

        public string FullName => !string.IsNullOrEmpty(Patronymic)
            ? $"{LastName} {FirstName} {Patronymic}"
            : $"{LastName} {FirstName}";

        public bool MatchToSearchString(string searchString) =>
            string.IsNullOrEmpty(searchString) || searchString.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries).All(Match);

        private bool Match(string search)
        {
            return FirstName.ToLower().Contains(search)
                   || LastName.ToLower().Contains(search)
                   || Patronymic.ToLower().Contains(search)
                   || Phone.ToLower().Contains(search)
                   || Email.ToLower().Contains(search);
        }

        public bool MatchToFilter(UserFilter filter)
        {
            return (UserPrivilege == "ADMIN" && filter.IncludeAdmins ||
                    UserPrivilege == "CLIENT" && filter.IncludeClients)
                   && (UserStatus == "LOCKED" && filter.IncludeBanned ||
                       UserStatus == "DELETED" && filter.IncludeDeleted ||
                       UserStatus == "ACTIVE");
        }
    }
}