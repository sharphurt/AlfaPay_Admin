using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using AlfaPay_Admin.Annotations;
using AlfaPay_Admin.Entity;
using AlfaPay_Admin.Enum;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Application = System.Windows.Application;
using RestClient = RestSharp.RestClient;

namespace AlfaPay_Admin.Context
{
    public class ApiRequestManager<T> : INotifyPropertyChanged
    {
        private RequestStatus _status;

        public RequestStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private ApiResponse<T> _response;

        public ApiResponse<T> Response
        {
            get => _response;
            set
            {
                _response = value;
                OnPropertyChanged(nameof(Response));
            }
        }

        private RestSharp.RestClient _client;

        public ApiRequestManager()
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            _client = new RestSharp.RestClient(baseUrl);
            _client.UseNewtonsoftJson();
        }

        public async void MakeRequest(Method method, string path, object toJson, Action onSuccessful, Action onError)
        {
            var request = new RestRequest(path);
            if (!(toJson is null))
                request.AddJsonBody(toJson);
            if (AuthenticationContext.Token != null)
                request.AddHeader("Authorization", AuthenticationContext.Token.ToString());

            request.Timeout = 20 * 1000;

            Status = RequestStatus.InProgress;
            var response = await _client.ExecuteAsync<ApiResponse<T>>(request, method);
            if (response.StatusCode == 0)
                Response = new ApiResponse<T>(default,
                    new ApiError("Произошла ошибка. Проверьте ваше интернет-соединение и повторите попытку", 504,
                        HttpStatusCode.GatewayTimeout.ToString()));
            else
                Response = response.Data;
            if (Response.IsSuccessfully)
            {
                Status = RequestStatus.CompletedSuccessfully;
                onSuccessful?.Invoke();
            }
            else
            {
                Status = RequestStatus.CompletedWithError;
                onError?.Invoke();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}