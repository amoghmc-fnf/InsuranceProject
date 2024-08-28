using System.Net.Http;
using MyClientApp.Client;
using System.Net.Http.Json;

namespace MyClientApp.Services
{
    public interface IPolicyService
    {
        Task Add(PolicyDto employee);
        Task<PolicyDto> AddAndGet(PolicyDto employee);
        Task Delete(int id);
        Task<List<PolicyDto>> GetAll();
        Task<PolicyDto> GetById(int id);
        Task Update(PolicyDto employee);
    }

    public class PolicyService : IPolicyService
    {
        private readonly HttpClient httpClient;
        public PolicyService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(Program.Configuration["PolicyApiUrl"]);
        }

        public async Task<List<PolicyDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<PolicyDto>>("Policy");
        }

        public async Task<PolicyDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<PolicyDto>($"Policy/{id}");
        }

        public async Task Add(PolicyDto employee)
        {
            await httpClient.PostAsJsonAsync<PolicyDto>("Policy", employee);
        }

        public async Task<PolicyDto> AddAndGet(PolicyDto employee)
        {
            var response = await httpClient.PostAsJsonAsync<PolicyDto>("Policy", employee);
            return await response.Content.ReadFromJsonAsync<PolicyDto>();
        }

        public async Task Delete(int id)
        {
            await httpClient.DeleteAsync($"Policy/{id}");
        }

        public async Task Update(PolicyDto employee)
        {
            await httpClient.PutAsJsonAsync<PolicyDto>("Policy", employee);
        }
    }
}
