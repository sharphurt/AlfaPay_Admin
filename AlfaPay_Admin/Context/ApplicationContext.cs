using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AlfaPay_Admin.Model
{
    class ApplicationContext
    {
        private readonly HttpClient _client = new HttpClient();

        public ApplicationContext(string uri)
        {
            _client.BaseAddress = new Uri(uri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<string> GetAsync(string path)
        {
            return await _client.GetStringAsync(path);
        }

        public async Task<string> SendApiRequest(string path, string token)
        {
            try
            {
                if (token != null)
                    _client.DefaultRequestHeaders.Add("Authorization", token);
                return await GetAsync(path);
            }
            catch (Exception e)
            {
                return "Error";
            }
        }
    }
}