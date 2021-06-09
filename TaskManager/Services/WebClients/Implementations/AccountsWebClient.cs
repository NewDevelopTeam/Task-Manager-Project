using System;
using System.Net.Http;
using PlusDashData;
using TaskManager.Services.WebClients.Interfaces;

namespace TaskManager.Services.WebClients.Implementations
{
    public class AccountsWebClient : WebClientPattern, IAccountsWebClient
    {
        public AccountsWebClient(HttpClient client) : base(client)
        {
            client.BaseAddress = new Uri("https://localhost:44397/");
        }
    }
}
