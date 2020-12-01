using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AlfaPay_Admin.Context;
using AlfaPay_Admin.Entity;

namespace AlfaPay_Admin.Model
{
    class ApplicationContext
    {
        private readonly HttpClient _client = new HttpClient();

        public ApplicationContext(string uri)
        {
            _client.BaseAddress = new Uri(uri);
            _client.Timeout = TimeSpan.FromMinutes(2);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<string> GetAsync(string path)
        {
            return await _client.GetStringAsync(path);
        }

        public async Task<ApiResponse<string>> SendApiRequest(string path, string token)
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
                return new ApiResponse<string>(null,
                    new ApiError(e.InnerException.Message, e.HResult,
                        (e.InnerException as WebException).Status.ToString()));
            }
        }

        public async Task<ApiResponse<ObservableCollection<ClientApplication>>> LoadApplications(int from, int count)
        {
            var result = await SendApiRequest($"api/applications/get?from={from}&count={count}", null);
            if (!result.IsSuccessfully)
                return new ApiResponse<ObservableCollection<ClientApplication>>(null, result.Error);

            return Deserializer.DeserializeApiResponse<ObservableCollection<ClientApplication>>(result.Response);
        }

        /*
        public async Task<bool?> CheckProperty(string name, string email)
        {
            var result = await SendApiRequest($"api/auth/check/{name}?{name}={email}", null);
            if (result.StartsWith("Error"))
                return null;
            var response = Deserializer.DeserializeApiResponse<bool>(result);
            return response.Response;
        }*/
    }
}