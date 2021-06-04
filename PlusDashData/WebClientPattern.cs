using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlusDashData
{
    public class WebClientPattern
    {
        private static HttpClient _client;
        public WebClientPattern(HttpClient client)
        {
            _client = client;
        }
        public async Task<string> GetAsync(string path, string arg)
        {
            var request = await _client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(_client.BaseAddress, path + arg),
                Method = HttpMethod.Get
            });

            return await request.Content.ReadAsStringAsync();
        }
        public async Task<string> PostAsync<T>(string path, T arg)
        {
            var jsonValues = JsonConvert.SerializeObject(arg);
            var request = await _client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(_client.BaseAddress, path),
                Method = HttpMethod.Post,
                Content = new StringContent(jsonValues, Encoding.UTF8, "application/json")
            });

            return await request.Content.ReadAsStringAsync();
        }
        public async Task<string> PutAsync(string path, string args)
        {
            var request = await _client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(_client.BaseAddress, path + args),
                Method = HttpMethod.Put
            });

            return await request.Content.ReadAsStringAsync();
        }
        public async Task<string> DeleteAsync(string path, string args)
        {
            var requestResult = await _client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(_client.BaseAddress, path + args),
                Method = HttpMethod.Delete
            });

            return await requestResult.Content.ReadAsStringAsync();
        }
    }
}
