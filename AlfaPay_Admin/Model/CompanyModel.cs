using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AlfaPay_Admin.Model
{
    public class CompanyModel : IDataErrorInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string TaxSystem { get; set; }
        public string Kkt { get; set; }

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
                    case "Inn":
                        if (!Regex.IsMatch(Inn, "[\\d]{10}"))
                            return "Введите 10 цифр";
                        break;
                    case "TaxSystem":
                        if (!Regex.IsMatch(TaxSystem, "ЕНВД|ЕСХН|СРП|УСН|ЕНВД-ЕСХН|УСН-ЕНВД|ОСН"))
                            return "Некорректный тип СНО";
                        break;
                    case "Kkt":
                        if (!Regex.IsMatch(Inn, "[\\d]{16}"))
                            return "Введите 16 цифр";
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