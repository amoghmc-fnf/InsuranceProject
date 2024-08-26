using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AdminApp.Models;

namespace AdminApp.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly HttpClient _httpClient;

        public HospitalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Hospital>> GetAllHospitalsAsync()
        {
            var response = await _httpClient.GetAsync("Hospital");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var hospitals = JsonSerializer.Deserialize<List<Hospital>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return hospitals;
        }

        public async Task<List<Hospital>>AddHospitalAsync(Hospital hospital)
        {
            var response = await _httpClient.PostAsJsonAsync("Hospital", hospital);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Hospital>>();
        }

        public async Task UpdateHospitalAsync(Hospital hospital)
        {
            var response = await _httpClient.PutAsJsonAsync($"Hospital/{hospital.HospitalId}", hospital);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteHospitalAsync(int hospitalId)
        {
            var response = await _httpClient.DeleteAsync($"Hospital/{hospitalId}");
            response.EnsureSuccessStatusCode();
        }
    }

    public interface IHospitalService
    {
        Task<List<Hospital>> GetAllHospitalsAsync();
        Task<List<Hospital>> AddHospitalAsync(Hospital hospital);
        Task UpdateHospitalAsync(Hospital hospital);
        Task DeleteHospitalAsync(int hospitalId);
    }
}