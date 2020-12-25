using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlfaPay_Admin.Model
{
    public class ClientModel : IDataErrorInfo
    {
        [JsonProperty("username")] public string Username => Email.Split('@')[0] + "testttt";

        [JsonProperty("firstName")] public string Name { get; set; }

        [JsonProperty("lastName")] public string Surname { get; set; }

        [JsonIgnore] public bool HavePatronymic => true;

        private string _patronymic;

        [JsonProperty("patronymic")]
        public string Patronymic
        {
            get
            {
                return string.IsNullOrEmpty(_patronymic) ? "" : _patronymic;
            }
            set => _patronymic = value;
        }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("password")] public string Password => Membership.GeneratePassword(15, 2);
        
        [JsonProperty("privilege")]
        public string Privilege => "CLIENT";

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        if (!Regex.IsMatch(Name, "[А-Яа-яЁё]{2,}"))
                            return "Введите минимум 2 кириллических символа";
                        break;
                    case "Surname":
                        if (!Regex.IsMatch(Surname, "[А-Яа-яЁё\\-]{2,}"))
                            return "Введите минимум 2 кириллических символа";
                        break;
                    case "Patronymic":
                        if (!Regex.IsMatch(Patronymic, "[А-Яа-яЁё]{0,}") && HavePatronymic)
                            return "Введите минимум 2 кириллических символа";
                        break;
                    case "Email":
                        if (!Regex.IsMatch(Email, "[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+"))
                            return "Некорректный адрес электронной почты";
                        break;
                    case "Phone":
                        if (!Regex.IsMatch(Phone, "\\+7[\\d]{10}"))
                            return "Некорректный номер телефона";
                        break;
                    default:
                        return string.Empty;
                }

                return string.Empty;
            }
        }

        public string Error { get; }
    }
}