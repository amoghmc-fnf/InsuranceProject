using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IPolicyHolderService
    {
        Task Add(PolicyHolderDto employee);
        Task DeleteById(int id);
        Task<List<PolicyHolderDto>> GetAll();
        Task<PolicyHolderDto> GetById(int id);
        Task Update(PolicyHolderDto employee);
    }

    public class PolicyHolderService : IPolicyHolderService
    {
        private readonly HttpClient httpClient;
        public PolicyHolderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<PolicyHolderDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<PolicyHolderDto>>("PolicyHolder");
        }

        public async Task<PolicyHolderDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<PolicyHolderDto>($"PolicyHolder/{id}");
        }

        public async Task Add(PolicyHolderDto employee)
        {
            await httpClient.PostAsJsonAsync<PolicyHolderDto>("PolicyHolder", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"PolicyHolder/{id}");
        }

        public async Task Update(PolicyHolderDto employee)
        {
            await httpClient.PutAsJsonAsync<PolicyHolderDto>("PolicyHolder", employee);
        }
    }
}
