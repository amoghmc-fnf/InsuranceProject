using Newtonsoft.Json.Linq;
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
        private readonly IAuthService _authService;
        private string token;

        public ClaimService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task AddTokenJwtTokenHeader()
        {
            if (token is null)
            {
                token = await _authService.GetJwtToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
        }

        public async Task<List<ClaimDto>> GetAll()
        {
            AddTokenJwtTokenHeader();
            return await _httpClient.GetFromJsonAsync<List<ClaimDto>>("Claim");
        }

        public async Task<ClaimDto> GetById(int id)
        {
            AddTokenJwtTokenHeader();
            return await _httpClient.GetFromJsonAsync<ClaimDto>($"Claim/{id}");
        }

        public async Task Add(ClaimDto employee)
        {
            AddTokenJwtTokenHeader();
            await _httpClient.PostAsJsonAsync<ClaimDto>("Claim", employee);
        }

        public async Task DeleteById(int id)
        {
            AddTokenJwtTokenHeader();
            await _httpClient.DeleteAsync($"Claim/{id}");
        }

        public async Task Update(ClaimDto employee)
        {
            AddTokenJwtTokenHeader();
            await _httpClient.PutAsJsonAsync<ClaimDto>("Claim", employee);
        }

        public async Task<List<ClaimDto>> GetAllClaimsAsync()
        {
            AddTokenJwtTokenHeader();
            string endpoint = "Claim";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var claims = JsonSerializer.Deserialize<List<ClaimDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return claims;
        }

        public async Task UpdateClaimStatusAsync(int claimId, string status, decimal dispenseAmount)
        {
            AddTokenJwtTokenHeader();
            var updateDto = new { Status = status, DispenseAmount = dispenseAmount };
            var response = await _httpClient.PutAsJsonAsync($"Claim/{claimId}/status", updateDto);
            response.EnsureSuccessStatusCode();
        }

    }
}