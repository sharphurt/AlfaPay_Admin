using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using DeviceId;
using Flurl.Http;

namespace AlfaPay_Admin.Model
{
    public sealed class LoginModel : INotifyPropertyChanged
    {
        private DeviceInfo DeviceInfo { get; }

        private JwtContainer _token;

        public JwtContainer Token
        {
            get => _token;
            set
            {
                _token = value;
                OnPropertyChanged(nameof(Token));
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool _responseReceived;

        public bool ResponseReceived
        {
            get => _responseReceived;
            set
            {
                _responseReceived = value;
                OnPropertyChanged(nameof(ResponseReceived));
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private ApiError _error;

        public ApiError Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }
        
        private RelayCommand _loginCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(obj =>
                {
                    Login(Email, Password);
                }));
            }
        }

        public LoginModel()
        {
            var deviceId = new DeviceIdBuilder()
                .AddMachineName()
                .AddMacAddress()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .ToString();
            DeviceInfo = new DeviceInfo(deviceId);
        }

        private async void Login(string _email, string _password)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            try
            {
                var result = await $"{baseUrl}/auth/login".PostJsonAsync(new
                {
                    email = _email,
                    password = _password,
                    deviceInfo = DeviceInfo
                }).ReceiveJson<ApiResponse<JwtContainer>>();

                Token = result.Response;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                Error = new ApiError("Проблемы с интернет-соединением. Проверьте подключение и попробуйте снова", 504,
                    "");
            }
            catch (FlurlHttpException ex)
            {
                var response = await ex.GetResponseJsonAsync<ApiResponse<object>>();
                Error = response is null ? new ApiError(ex.Call.Exception.Message, 0, "") : response.Error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}