using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AlfaPay_Admin.Model
{
    public class UserModel : IDataErrorInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool HavePatronymic { get => true; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        if (!Regex.IsMatch(Name, "[А-Яа-яЁё\\-]{2,}"))
                            return "Имя может содержать только русские буквы и символы дефиса";
                        break;
                    case "Surname":
                        if (!Regex.IsMatch(Surname, "[А-Яа-яЁё\\-]{2,}"))
                            return "Фамилия может содержать только русские буквы и символы дефиса";
                        break;
                    case "Patronynic":
                        if (!Regex.IsMatch(Surname, "[А-Яа-яЁё\\-]{2,}") && HavePatronymic)
                            return "Отчество может содержать только русские буквы и символы дефиса";
                        break;
                    case "Email":
                        if (!Regex.IsMatch(Surname, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
                            return "Некорректный адрес электронной почты";
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