using MyClientApp.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MyClientApp.Client;

namespace MyClientApp.Services
{
    public interface IPolicyHolderDtoService
    {
        Task Add(PolicyHolderDto employee);
        Task DeleteById(int id);
        Task<List<PolicyHolderDto>> GetAll();
        Task<PolicyHolderDto> GetById(int id);
        Task Update(PolicyHolderDto employee);

        Task<string> LoginAsync(LoginDto loginModel);
        Task<PolicyHolderDto> GetPolicyHolderByEmailAsync(string email);

    }

    public class PolicyHolderDtoService : IPolicyHolderDtoService
    {
        private readonly HttpClient httpClient;
        public PolicyHolderDtoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(Program.Configuration["UserApiUrl"]);
        }

        public async Task<List<PolicyHolderDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<PolicyHolderDto>>("PolicyHolder");
        }

        public async Task<PolicyHolderDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<PolicyHolderDto>($"PolicyHolder/{id}");
        }

        public async Task Add(PolicyHolderDto employee)
        {
            await httpClient.PostAsJsonAsync<PolicyHolderDto>("PolicyHolder", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"PolicyHolder/{id}");
        }

        public async Task Update(PolicyHolderDto employee)
        {
            await httpClient.PutAsJsonAsync<PolicyHolderDto>("PolicyHolder", employee);
        }
        public async Task<string> LoginAsync(LoginDto loginModel)
        {
            var response = await httpClient.PostAsJsonAsync("PolicyHolder/authenticate", loginModel);

            if (response.IsSuccessStatusCode)
            {
                // Assuming the response contains a token or other session management data
                var result = await response.Content.ReadAsStringAsync();
                return result; // Return the token or relevant data
            }

            return null; // Login failed
        }
        public async Task<PolicyHolderDto> GetPolicyHolderByEmailAsync(string email)
        {
            var response = await httpClient.GetAsync($"PolicyHolder/getByEmail?email={email}");

            if (response.IsSuccessStatusCode)
            {
                var policyHolder = await response.Content.ReadFromJsonAsync<PolicyHolderDto>();
                return policyHolder;
            }

            return null; // No policy holder found with the given email
        }


    }
}