using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AdminApp.Services;
public class PolicyHolderService
{
    private readonly HttpClient _httpClient;

    public PolicyHolderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PolicyHolderDto>> GetPolicyHoldersAsync()
    {
        string endpoint = "PolicyHolder";
        var response = await _httpClient.GetAsync(endpoint);
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

        var response = await _httpClient.PutAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
    }
}