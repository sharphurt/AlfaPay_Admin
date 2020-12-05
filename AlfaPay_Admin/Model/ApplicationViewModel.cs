using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Web.UI.WebControls;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using Flurl.Http;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Application = AlfaPay_Admin.Entity.Application;

namespace AlfaPay_Admin.Model
{
    public sealed class ApplicationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Application> _applications;
        private Application _selectedApplication;

        public ObservableCollection<Application> Applications
        {
            get => _applications;
            private set
            {
                _applications = value;
                OnPropertyChanged(nameof(Applications));
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = value;
        }

        public Application SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged(nameof(SelectedApplication));
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
        
        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand(obj =>
                {
                    GetApplicationsFromServer(0, 10);
                }));
            }
        }

        private RelayCommand _logoutCommand;

        public RelayCommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new RelayCommand(obj =>
                {
                    Logout();
                }));
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


        public ApplicationViewModel()
        {
            IsSuccessfully = true;
            LoggedInUser = AuthenticationContext.LoggedUser;
            GetApplicationsFromServer(0, 10);
        }
        
        private async void GetApplicationsFromServer(int from, int count)
        {
            ResponseReceived = false;
            Error = null;
            IsLoading = true;
            IsSuccessfully = true;
            try
            {
                var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
                var response = await $"{baseUrl}/applications/get?from={from}&count={count}"
                    .WithTimeout(30)
                    .GetJsonAsync<ApiResponse<ObservableCollection<Application>>>();
                Applications = response.Response;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                IsSuccessfully = false;
                Error = new ApiError("Проблемы с интернет-соединением. Проверьте подключение и попробуйте снова", 504,
                    "");
            }
            catch (FlurlHttpException ex)
            {
                var response = await ex.GetResponseJsonAsync<ApiResponse<object>>();
                IsSuccessfully = false;
                Error = response is null ? new ApiError(ex.Call.Exception.Message, 0, "") : response.Error;
            }
            
            ResponseReceived = true;
            IsLoading = false;
        }

        private async void Logout()
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            var client = new RestSharp.RestClient(baseUrl);
            client.UseNewtonsoftJson();
            var request = new RestRequest("auth/logout");
            request.AddJsonBody(new { deviceInfo = AuthenticationContext.DeviceInfo }, "application/json");
            request.AddHeader("Authorization", AuthenticationContext.Token.ToString());
            var response = await client.PostAsync<ApiResponse<string>>(request);
            if (!response.IsSuccessfully)
                Error = response.Error;
            else
            {
                LoggedInUser = null;
                AuthenticationContext.ClearAuthentication();
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