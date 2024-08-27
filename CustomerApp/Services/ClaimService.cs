using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IClaimService
    {
        Task Add(ClaimDto employee);
        Task DeleteById(int id);
        Task<List<ClaimDto>> GetAll();
        Task<ClaimDto> GetById(int id);
        Task Update(ClaimDto employee);
    }

    public class ClaimService : IClaimService
    {
        private readonly HttpClient httpClient;
        public ClaimService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ClaimDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<ClaimDto>>("Claim");
        }

        public async Task<ClaimDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<ClaimDto>($"Claim/{id}");
        }

        public async Task Add(ClaimDto employee)
        {
            await httpClient.PostAsJsonAsync<ClaimDto>("Claim", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Claim/{id}");
        }

        public async Task Update(ClaimDto employee)
        {
            await httpClient.PutAsJsonAsync<ClaimDto>("Claim", employee);
        }
    }
}
