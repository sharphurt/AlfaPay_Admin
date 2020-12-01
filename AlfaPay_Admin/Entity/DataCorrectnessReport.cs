namespace AlfaPay_Admin.Entity
{
    public class DataCorrectnessReport
    {
        public bool IsEmailCorrect { get; }
        public bool IsPhoneCorrect { get; }
        public bool IsInnCorrect { get; }

        public DataCorrectnessReport(bool isEmailCorrect, bool isPhoneCorrect, bool isInnCorrect)
        {
            IsEmailCorrect = isEmailCorrect;
            IsPhoneCorrect = isPhoneCorrect;
            IsInnCorrect = isInnCorrect;
        }
    }
}