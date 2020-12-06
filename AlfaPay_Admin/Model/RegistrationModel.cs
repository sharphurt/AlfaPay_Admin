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
    public sealed class RegistrationModel : ApiRequestManager<string>, INotifyPropertyChanged
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

        /*
        private ApiResponse _registrationResponse;

        public ApiResponse RegistrationResponse
        {
            get => _registrationResponse;
            set
            {
                _registrationResponse = value;
                OnPropertyChanged(nameof(RegistrationResponse));
            }
        }
        */


        private RelayCommand _registerCommand;

        public RelayCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new RelayCommand(obj => { RegisterClientCompany(); }));


        public RegistrationModel(Application application)
        {
            Application = application;
            ClientModel = new ClientModel();
            CompanyModel = new CompanyModel();
        }

        private void RegisterClientCompany()
        {
            var testClient = new ClientModel()
            {
                Email = "testhui1@test.hui",
                Name = "Тест",
                Surname = "Тестов",
                Patronymic = "Тестович",
                Phone = "+79999919949"
            };

            var testCompany = new CompanyModel()
            {
                Address = "екатеринбург коминтерна 5",
                Inn = "1234567890",
                Name = "CatstackTest",
                Kkt = "2302404920492222",
                TaxSystem = "ОСН"
            };

            /*
            MakeRequest(Method.POST, "/auth/register", new
            {
                client = testClient,
                company = testCompany
            });*/
        }

        /*
        private async Task<ApiResponse<string>> TryRegisterEntity(object entity, string path)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var client = new RestSharp.RestClient(baseUrl);
            client.UseNewtonsoftJson();

            var request = new RestRequest(path);
            request.AddJsonBody(entity, "application/json");
            request.AddHeader("Authorization", AuthenticationContext.Token.ToString());
            var response = await client.PostAsync<ApiResponse<string>>(request);
            if (!response.IsSuccessfully)
                Error = response.Error;

            return response;
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}