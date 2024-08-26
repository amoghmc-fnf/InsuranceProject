using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly HttpClient _httpClient;

        public InsuranceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<InsuranceTypeDto>> GetActivePlansAsync()
        {
            string endpoint = "InsuranceType";
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var plans = JsonSerializer.Deserialize<List<InsuranceTypeDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return plans;
        }

        public async Task<bool> UpdatePlanAsync(InsuranceTypeDto plan)
        {
            var response = await _httpClient.PutAsJsonAsync("InsuranceType", plan);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddPlanAsync(InsuranceTypeDto newPlan)
        {
            string endpoint = "InsuranceType"; // Assuming this is the correct endpoint for adding a plan
            var response = await _httpClient.PostAsJsonAsync(endpoint, newPlan);

            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeletePlanAsync(int insuranceId)
        {
            var response = await _httpClient.DeleteAsync($"InsuranceType/{insuranceId}");
            return response.IsSuccessStatusCode;
        }
    }
}
public interface IInsuranceService
{
    Task<List<InsuranceTypeDto>> GetActivePlansAsync();
    Task<bool> UpdatePlanAsync(InsuranceTypeDto plan);
    Task<bool> AddPlanAsync(InsuranceTypeDto newPlan);
    Task<bool> DeletePlanAsync(int insuranceId);
}