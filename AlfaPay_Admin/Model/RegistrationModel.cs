using System.ComponentModel;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Entity;

namespace AlfaPay_Admin.Model
{
    public sealed class RegistrationModel: INotifyPropertyChanged
    {
        private UserModel _userModel;

        public UserModel UserModel
        {
            get => _userModel;
            set
            {
                _userModel = value;
                OnPropertyChanged(nameof(UserModel));
            }
        }

        private CompanyModel _companyModel;
        public CompanyModel CompanyModel {
            get => _companyModel;
            set
            {
                _companyModel = value;
                OnPropertyChanged(nameof(CompanyModel));
            }
        }

        private Application _application;
        public Application Application
        {
            get => _application;
            set
            {
                _application = value;
                OnPropertyChanged(nameof(Application));
            }
        }

        public RegistrationModel(UserModel userModel, CompanyModel companyModel, Application application)
        {
            Application = application;
            UserModel = new UserModel();
            CompanyModel = new CompanyModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}