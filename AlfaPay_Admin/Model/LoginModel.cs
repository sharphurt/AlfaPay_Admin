using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using AlfaPay_Admin.Enum;
using DeviceId;
using Flurl.Http;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Application = System.Windows.Application;

namespace AlfaPay_Admin.Model
{
    public sealed class LoginModel : INotifyPropertyChanged, IDataErrorInfo
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

        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??= new RelayCommand(SendLoginRequest);
            }
        }

        private LoggedUser _loggedInUser;

        public LoggedUser LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }

        private ApiRequestManager<JwtContainer> _loginRequestManager;

        public ApiRequestManager<JwtContainer> LoginRequestManager
        {
            get => _loginRequestManager;
            set
            {
                _loginRequestManager = value;
                OnPropertyChanged(nameof(LoginRequestManager));
            }
        }

        private ApiRequestManager<LoggedUser> _userInformationRequestManager;

        public ApiRequestManager<LoggedUser> UserInformationRequestManager
        {
            get => _userInformationRequestManager;
            set
            {
                _userInformationRequestManager = value;
                OnPropertyChanged(nameof(UserInformationRequestManager));
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

        public LoginModel()
        {
            var deviceId = new DeviceIdBuilder()
                .AddMachineName()
                .AddMacAddress()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .ToString();
            AuthenticationContext.DeviceInfo = new DeviceInfo(deviceId);
            LoginRequestManager = new ApiRequestManager<JwtContainer>();
            UserInformationRequestManager = new ApiRequestManager<LoggedUser>();
        }

        private void OnSuccessfulLogin()
        {
            AuthenticationContext.Token = LoginRequestManager.Response.Response;
            Token = LoginRequestManager.Response.Response;
            GetUserInformationRequest();
        }

        private void OnSuccessfulGettingUserProfile()
        {
            AuthenticationContext.LoggedUser = UserInformationRequestManager.Response.Response;
            LoggedInUser = AuthenticationContext.LoggedUser;
        }

        private void SendLoginRequest(object obj)
        {
            LoginRequestManager.MakeRequest(Method.POST, "auth/login", new
            {
                email = Login,
                password = Password,
                deviceInfo = AuthenticationContext.DeviceInfo
            }, OnSuccessfulLogin, () =>
            {
                ErrorMessage = LoginRequestManager.Response.Error.Message;
            });
        }

        private void GetUserInformationRequest()
        {
            UserInformationRequestManager.MakeRequest(Method.GET, "users/me", null, OnSuccessfulGettingUserProfile,
                () => { ErrorMessage = UserInformationRequestManager.Response.ToString(); });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Login":
                        if (Login == "")
                            return "Введите логин";
                        break;
                    case "Password":
                        if (Password == "")
                            return "Введите пароль";
                        break;
                }

                return string.Empty;
            }
        }

        public string Error { get; }
    }
}