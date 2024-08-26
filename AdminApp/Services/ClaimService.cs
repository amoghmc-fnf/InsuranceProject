using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace AdminApp.Services
{
    public class ClaimService : IClaimService
    {
        private readonly HttpClient _httpClient;

        public ClaimService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

    public interface IClaimService
    {
        Task<List<ClaimDto>> GetAllClaimsAsync();
        Task UpdateClaimStatusAsync(int claimId, string status, decimal dispenseAmount);
    }
}