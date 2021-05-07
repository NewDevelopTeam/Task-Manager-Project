using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace PlusDashData.Data
{
    public class WebClient
    {
        private static HttpClient client;
        private string URL;

        public WebClient(string URL)
        {
            client = new HttpClient();
            this.URL = URL;
            var servicePoint = System.Net.ServicePointManager.FindServicePoint(new Uri(URL));
            servicePoint.ConnectionLeaseTimeout = 0;
        }
        public string Send<T>(HttpMethod method, string path, T arg)
        {
            var jsonValues = JsonConvert.SerializeObject(arg);
            var requestResult = client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(URL + '/' + path),
                Method = method,
                Content = new StringContent(jsonValues, Encoding.UTF8, "application/json")
            }).Result;

            return requestResult.Content.ReadAsStringAsync().Result;
        }
        public string Get(string path, string argsInString)
        {
            var requestResult = client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(URL + '/' + path + argsInString),
                Method = HttpMethod.Get
            }).Result;

            return requestResult.Content.ReadAsStringAsync().Result;
        }
        public string Put(string path, string argsInString)
        {
            var requestResult = client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(URL + '/' + path + argsInString),
                Method = HttpMethod.Put
            }).Result;

            return requestResult.Content.ReadAsStringAsync().Result;
        }
        public string Delete(string path, string argsInString)
        {
            var requestResult = client.SendAsync(new HttpRequestMessage()
            {
                RequestUri = new Uri(URL + '/' + path + argsInString),
                Method = HttpMethod.Delete
            }).Result;

            return requestResult.Content.ReadAsStringAsync().Result;
        }
    }
}
