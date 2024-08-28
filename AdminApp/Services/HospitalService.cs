using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AdminApp.Models;
using Newtonsoft.Json.Linq;

namespace AdminApp.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        private string token;

        public HospitalService(HttpClient httpClient, IAuthService authService)
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

        public async Task<List<Hospital>> GetAllHospitalsAsync()
        {
            AddTokenJwtTokenHeader();
            var response = await _httpClient.GetAsync("Hospital");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var hospitals = JsonSerializer.Deserialize<List<Hospital>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return hospitals;
        }

        public async Task AddHospitalAsync(Hospital hospital)
        {
            AddTokenJwtTokenHeader();
            var response = await _httpClient.PostAsJsonAsync("Hospital", hospital);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateHospitalAsync(Hospital hospital)
        {
            AddTokenJwtTokenHeader();
            var response = await _httpClient.PutAsJsonAsync("Hospital", hospital);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteHospitalAsync(int hospitalId)
        {
            AddTokenJwtTokenHeader();
            var response = await _httpClient.DeleteAsync($"Hospital/{hospitalId}");
            response.EnsureSuccessStatusCode();
        }
    }

    public interface IHospitalService
    {
        Task<List<Hospital>> GetAllHospitalsAsync();
        Task AddHospitalAsync(Hospital hospital);
        Task UpdateHospitalAsync(Hospital hospital);
        Task DeleteHospitalAsync(int hospitalId);
    }
}