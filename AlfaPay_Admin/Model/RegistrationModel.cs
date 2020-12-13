using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using RestClient = RestSharp.RestClient;

namespace AlfaPay_Admin.Model
{
    public sealed class RegistrationModel : INotifyPropertyChanged
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

        public CompanyModel CompanyModel
        {
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

        private ApiRequestManager<string> _registrationRequestManager;

        public ApiRequestManager<string> RegistrationRequestManager
        {
            get => _registrationRequestManager;
            set
            {
                _registrationRequestManager = value;
                OnPropertyChanged(nameof(RegistrationRequestManager));
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private bool _isSuccessfully;

        public bool IsSuccessfully
        {
            get => _isSuccessfully;
            set
            {
                _isSuccessfully = value;
                OnPropertyChanged(nameof(IsSuccessfully));
            }
        }
        
        private RelayCommand _registerCommand;

        public RelayCommand RegisterCommand =>
            _registerCommand ??= new RelayCommand(obj => { RegisterClientCompany(); });

        private RelayCommand _openApplyWindowCommand;

        public RelayCommand OpenApplyWindowCommand =>
            _openApplyWindowCommand ??= new RelayCommand(obj => { OpenApplyWindow(); });

        private void OpenApplyWindow()
        {
            RegistrationRequestManager.Reset();
        }
        
        
        public RegistrationModel(Application application)
        {
            Application = application;
            ClientModel = new ClientModel();
            CompanyModel = new CompanyModel();
            RegistrationRequestManager = new ApiRequestManager<string>();
        }

        private void RegisterClientCompany()
        {
            var testClient = new ClientModel()
            {
                Email = "te7t222@t66st.hui",
                Name = "Тест",
                Surname = "Тестов",
                Patronymic = "Тестович",
                Phone = "+79922132226"
            };

            var testCompany = new CompanyModel()
            {
                Address = "екатеринбург коминтерна 5",
                Inn = "1242261097",
                Name = "Catstack7",
                TaxSystem = "ОСН"
            };
            

            RegistrationRequestManager.MakeRequest(Method.POST, "/auth/register", new
                {
                    client = ClientModel,
                    company = CompanyModel,
                    applicationToRemoveId = Application.Id
                }, () => { IsSuccessfully = true; },
                () => { ErrorMessage = RegistrationRequestManager.Response.Error.Message; });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}