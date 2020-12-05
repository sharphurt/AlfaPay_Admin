using System.ComponentModel;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Entity;

namespace AlfaPay_Admin.Model
{
    public sealed class RegistrationModel: INotifyPropertyChanged
    {
        private ClientModel _clientModel;

        public ClientModel ClientModel
        {
            get => _clientModel;
            set
            {
                _clientModel = value;
                OnPropertyChanged(nameof(ClientModel));
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

        public RegistrationModel(ClientModel clientModel, CompanyModel companyModel, Application application)
        {
            Application = application;
            ClientModel = new ClientModel();
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