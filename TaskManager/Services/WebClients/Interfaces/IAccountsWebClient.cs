using System.Threading.Tasks;

namespace TaskManager.Services.WebClients.Interfaces
{
    public interface IAccountsWebClient
    {
        public Task<string> PostAsync<T>(string path, T arg);
        public Task<string> GetAsync(string path, string args);
        public Task<string> PutAsync(string path, string args);
        public Task<string> DeleteAsync(string path, string args);
    }
}
