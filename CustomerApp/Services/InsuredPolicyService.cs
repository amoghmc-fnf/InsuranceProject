using InsuranceApi.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public class InsuredPolicyDtoService
    {
        private readonly HttpClient httpClient;
        public InsuredPolicyDtoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<InsuredPolicyDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<InsuredPolicyDto>>("InsuredPolicy");
        }

        public async Task<InsuredPolicyDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<InsuredPolicyDto>($"InsuredPolicy/{id}");
        }

        public async Task Add(InsuredPolicyDto employee)
        {
            await httpClient.PostAsJsonAsync<InsuredPolicyDto>("InsuredPolicy", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"InsuredPolicy/{id}");
        }

        public async Task Update(InsuredPolicyDto employee)
        {
            await httpClient.PutAsJsonAsync<InsuredPolicyDto>("InsuredPolicy", employee);
        }
    }
}
