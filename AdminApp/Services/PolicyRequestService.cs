using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public class PolicyRequestService : IPolicyRequestService
    {
        private readonly HttpClient _httpClient;

        public PolicyRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<InsuredPolicyDto>> GetInsuredPoliciesAsync()
        {
            var response = await _httpClient.GetAsync("InsuredPolicy");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<InsuredPolicyDto>>();
        }

        public async Task<PolicyHolderDto> GetPolicyHolderAsync(int policyHolderId)
        {
            var response = await _httpClient.GetAsync($"PolicyHolder/{policyHolderId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PolicyHolderDto>();
        }

        public async Task<PolicyDto> GetPolicyAsync(int policyId)
        {
            var response = await _httpClient.GetAsync($"Policy/{policyId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PolicyDto>();
        }

        public async Task<List<PaymentDto>> GetPaymentsByInsuredPolicyIdAsync(int insuredPolicyId)
        {
            var response = await _httpClient.GetAsync($"Payment?insuredPolicyId={insuredPolicyId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<PaymentDto>>();
        }

        public async Task<bool> UpdateApprovalStatusAsync(int insuredPolicyId, string approvalStatus)
        {
            var response = await _httpClient.PutAsJsonAsync($"InsuredPolicy/{insuredPolicyId}/ApprovalStatus", approvalStatus);
            return response.IsSuccessStatusCode;
        }
    }
}

public interface IPolicyRequestService
{
    Task<List<InsuredPolicyDto>> GetInsuredPoliciesAsync();
    Task<PolicyHolderDto> GetPolicyHolderAsync(int policyHolderId);
    Task<PolicyDto> GetPolicyAsync(int policyId);
    Task<List<PaymentDto>> GetPaymentsByInsuredPolicyIdAsync(int insuredPolicyId);
    Task<bool> UpdateApprovalStatusAsync(int insuredPolicyId, string approvalStatus);
}