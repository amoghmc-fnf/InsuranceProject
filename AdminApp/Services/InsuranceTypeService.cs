using System.Net.Http;
using System.Net.Http.Json;

namespace AdminApp.Services
{
    public interface IInsuranceTypeService
    {
        Task Add(InsuranceTypeDto employee);
        Task DeleteById(int id);
        Task<List<InsuranceTypeDto>> GetAll();
        Task<InsuranceTypeDto> GetById(int id);
        Task Update(InsuranceTypeDto employee);
    }

    public class InsuranceTypeService : IInsuranceTypeService
    {
        private readonly HttpClient httpClient;
        public InsuranceTypeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<InsuranceTypeDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<InsuranceTypeDto>>("InsuranceType");
        }

        public async Task<InsuranceTypeDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<InsuranceTypeDto>($"InsuranceType/{id}");
        }

        public async Task Add(InsuranceTypeDto employee)
        {
            await httpClient.PostAsJsonAsync<InsuranceTypeDto>("InsuranceType", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"InsuranceType/{id}");
        }

        public async Task Update(InsuranceTypeDto employee)
        {
            await httpClient.PutAsJsonAsync<InsuranceTypeDto>("InsuranceType", employee);
        }
    }
}
