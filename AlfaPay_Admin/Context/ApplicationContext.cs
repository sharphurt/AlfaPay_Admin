using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AlfaPay_Admin.Entity;
using AlfaPay_Admin.Model;

namespace AlfaPay_Admin.Context
{
    class ApplicationContext
    {
        private readonly HttpClient _client = new HttpClient();

        public ApplicationContext(string uri)
        {
            _client.BaseAddress = new Uri(uri);
            _client.Timeout = TimeSpan.FromSeconds(20);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<string> GetAsync(string path)
        {
            return await _client.GetStringAsync(path);
        }

        private async Task<ApiResponse<string>> SendApiRequest(string path, string token)
        {
            try
            {
                if (token == null)
                    return new ApiResponse<string>(await GetAsync(path), null);

                if (_client.DefaultRequestHeaders.Contains("Authorization"))
                    _client.DefaultRequestHeaders.Remove("Authorization");
                _client.DefaultRequestHeaders.Add("Authorization", token);

                return new ApiResponse<string>(await GetAsync(path), null);
            }
            catch (HttpRequestException e)
            {
                return new ApiResponse<string>(null, new ApiError("Проблемы с интернет-соединением. Попробуйте еще раз",
                    e.HResult, ""));
            }
            catch (TaskCanceledException e)
            {
                return new ApiResponse<string>(null,
                    new ApiError("Попробуйте еще раз", e.HResult, e.Message));
            }
        }

        private async Task<ApiResponse<T>> SendRequest<T>(string url, string token)
        {
            var result = await SendApiRequest(url, token);
            return !result.IsSuccessfully
                ? new ApiResponse<T>(default, result.Error)
                : Deserializer.DeserializeApiResponse<T>(result.Response);
        }

        public async Task<ApiResponse<ObservableCollection<Application>>> LoadApplications(int from, int count) =>
            await SendRequest<ObservableCollection<Application>>($"nfc-api/applications/get?from={from}&count={count}",
                null);
    }
}