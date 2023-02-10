using System.Net.Http.Json;

namespace DevTest.Client.Services
{
    public class RequestClient
    {
        private readonly HttpClient _httpClient;

        public RequestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<T> GetJsonAsync<T>(string url)
        {
            var response = await _httpClient.GetFromJsonAsync<T>(url);
            return response;
        }

        public async Task<int> PostJsonAsync<T>(string url, T model)
        {
            var response = await _httpClient.PostAsJsonAsync(url, model);
            if (response.IsSuccessStatusCode)
            {
                return int.Parse(await response.Content.ReadAsStringAsync());
            }

            return 0;
        }

        public async Task<bool> PutJsonAsync<T>(string url, T model)
        {
            var response = await _httpClient.PutAsJsonAsync(url, model);
            return true;
        }
    }
}
