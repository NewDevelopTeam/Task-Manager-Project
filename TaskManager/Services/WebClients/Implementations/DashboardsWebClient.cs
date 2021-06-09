using System;
using System.Net.Http;
using PlusDashData;
using TaskManager.Services.WebClients.Interfaces;

namespace TaskManager.Services.WebClients.Implementations
{
    public class DashboardsWebClient : WebClientPattern, IDashboardsWebClient
    {
        public DashboardsWebClient(HttpClient client): base(client)
        {
            client.BaseAddress = new Uri("https://localhost:44376/");
        }
    }
}