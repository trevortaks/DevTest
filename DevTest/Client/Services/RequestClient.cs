using DevTest.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace DevTest.Client.Services
{
    public class RequestClient
    {
        private readonly HttpClient _httpClient;

        public RequestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ResponseModel<T>?> GetJsonAsync<T>(string url)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseModel<T>>(url);
            return response;
        }

        public async Task<ResponseModel<T>?> PostJsonAsync<T>(string url, T model)
        {
            var response = await _httpClient.PostAsJsonAsync(url, model);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize(json, typeof(ResponseModel<T>), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) as ResponseModel<T>;

        }

        public async Task<ResponseModel<bool>?> PutJsonAsync<T>(string url, T model)
        {
            var response = await _httpClient.PutAsJsonAsync(url, model);
            return (ResponseModel<bool>?)JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(), typeof(ResponseModel<bool>));
        }

        public async Task<ResponseModel<bool>?> DeleteJsonAsync<T>(string url)
        {
            var response = await _httpClient.DeleteAsync(url );
            return (ResponseModel<bool>?)JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(), typeof(ResponseModel<bool>));
        }
    }
}
