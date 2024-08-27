using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace AdminApp.Services
{
    public interface IClaimService
    {
        Task Add(ClaimDto employee);
        Task DeleteById(int id);
        Task<List<ClaimDto>> GetAll();
        Task<List<ClaimDto>> GetAllClaimsAsync();
        Task<ClaimDto> GetById(int id);
        Task Update(ClaimDto employee);
        Task UpdateClaimStatusAsync(int claimId, string status, decimal dispenseAmount);
    }

    public class ClaimService : IClaimService
    {
        private readonly HttpClient _httpClient;

        public ClaimService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ClaimDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<ClaimDto>>("Claim");
        }

        public async Task<ClaimDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<ClaimDto>($"Claim/{id}");
        }

        public async Task Add(ClaimDto employee)
        {
            await _httpClient.PostAsJsonAsync<ClaimDto>("Claim", employee);
        }

        public async Task DeleteById(int id)
        {
            await _httpClient.DeleteAsync($"Claim/{id}");
        }

        public async Task Update(ClaimDto employee)
        {
            await _httpClient.PutAsJsonAsync<ClaimDto>("Claim", employee);
        }

        public async Task<List<ClaimDto>> GetAllClaimsAsync()
        {
            string endpoint = "Claim";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var claims = JsonSerializer.Deserialize<List<ClaimDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return claims;
        }

        public async Task UpdateClaimStatusAsync(int claimId, string status, decimal dispenseAmount)
        {
            var updateDto = new { Status = status, DispenseAmount = dispenseAmount };
            var response = await _httpClient.PutAsJsonAsync($"Claim/{claimId}/status", updateDto);
            response.EnsureSuccessStatusCode();
        }

    }
}