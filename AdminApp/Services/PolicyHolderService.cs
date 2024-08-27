using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace AdminApp.Services
{
    public interface IPolicyHolderService
    {
        Task Add(PolicyHolderDto employee);
        Task DeleteById(int id);
        Task<List<PolicyHolderDto>> GetAll();
        Task<PolicyHolderDto> GetById(int id);
        Task<List<PolicyHolderDto>> GetPolicyHoldersAsync();
        Task Update(PolicyHolderDto employee);
        Task UpdateStatusAsync(int id, int status);
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

        public async Task<List<PolicyHolderDto>> GetPolicyHoldersAsync()
        {
            string endpoint = "PolicyHolder";
            var response = await httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var customers = JsonSerializer.Deserialize<List<PolicyHolderDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return customers;
        }

        // Update this method to send status as an integer
        public async Task UpdateStatusAsync(int id, int status)
        {
            string endpoint = $"PolicyHolder/{id}/status";
            var content = new StringContent(JsonSerializer.Serialize(status), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
