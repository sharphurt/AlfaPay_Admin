using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;
using Flurl.Http;
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

        public ApplicationViewModel()
        {
            GetApplicationsFromServer(0, 10);
        }

        private async void GetApplicationsFromServer(int from, int count)
        {
            ResponseReceived = false;
            Error = null;
            IsLoading = true;
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
                Error = new ApiError("Проблемы с интернет-соединением. Проверьте подключение и попробуйте снова", 504, "");
            }
            catch (FlurlHttpException ex)
            {
                var response = await ex.GetResponseJsonAsync<ApiResponse<object>>();
                Error = response is null ? new ApiError(ex.Call.Exception.Message, 0, "") : response.Error;
            }


            ResponseReceived = true;
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}