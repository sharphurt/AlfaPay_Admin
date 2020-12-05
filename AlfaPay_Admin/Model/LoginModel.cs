using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using DeviceId;
using Flurl.Http;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Application = System.Windows.Application;

namespace AlfaPay_Admin.Model
{
    public sealed class LoginModel : INotifyPropertyChanged
    {
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

        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
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

        private bool _isLoggedInSuccessfully;

        public bool IsLoggedInSuccessfully
        {
            get => _isLoggedInSuccessfully;
            set
            {
                _isLoggedInSuccessfully = value;
                OnPropertyChanged(nameof(IsLoggedInSuccessfully));
            }
        }

        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new RelayCommand(obj => { LogInUser(); }));
            }
        }


        private LoggedUser _loggedInUser;

        public LoggedUser LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(_loggedInUser));
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
            AuthenticationContext.DeviceInfo = new DeviceInfo(deviceId);
        }

        private void LogInUser()
        {
            SendLoginRequest();
        }

        private async void SendLoginRequest()
        {
            ResponseReceived = false;
            Error = null;
            IsLoading = true;

            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            var client = new RestSharp.RestClient(baseUrl);
            client.UseNewtonsoftJson();
            var request = new RestRequest("auth/login");
            request.AddJsonBody(new
            {
                email = Login,
                password = Password,
                deviceInfo = AuthenticationContext.DeviceInfo
            }, "application/json");
            var response = await client.PostAsync<ApiResponse<JwtContainer>>(request);
            if (!response.IsSuccessfully)
                Error = response.Error;
            else
            {
                Token = response.Response;
                AuthenticationContext.Token = Token;
                if (Token != null)
                {
                    LoadUserInformation();
                }
            }

            ResponseReceived = true;
            IsLoading = false;
        }

        private async void LoadUserInformation()
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            try
            {
                var result = await $"{baseUrl}/users/me"
                    .WithHeader("Authorization", AuthenticationContext.Token)
                    .GetJsonAsync<ApiResponse<LoggedUser>>();
                LoggedInUser = result.Response;
                AuthenticationContext.LoggedUser = LoggedInUser;
                IsLoggedInSuccessfully = true;
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