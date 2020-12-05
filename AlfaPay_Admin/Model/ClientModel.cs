using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AlfaPay_Admin.Model
{
    public class ClientModel : IDataErrorInfo
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