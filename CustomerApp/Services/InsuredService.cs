using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IInsuredService
    {
        Task Add(InsuredDto employee);
        Task<InsuredDto> AddAndGet(InsuredDto employee);
        Task DeleteById(int id);
        Task<List<InsuredDto>> GetAll();
        Task<InsuredDto> GetById(int id);
        Task Update(InsuredDto employee);
    }

    public class InsuredService : IInsuredService
    {
        private readonly HttpClient httpClient;
        public InsuredService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<InsuredDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<InsuredDto>>("Insured");
        }

        public async Task<InsuredDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<InsuredDto>($"Insured/{id}");
        }

        public async Task Add(InsuredDto employee)
        {
            await httpClient.PostAsJsonAsync<InsuredDto>("Insured", employee);
        }

        public async Task<InsuredDto> AddAndGet(InsuredDto employee)
        {
            var response = await httpClient.PostAsJsonAsync<InsuredDto>("Insured", employee);
            return await response.Content.ReadFromJsonAsync<InsuredDto>();
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Insured/{id}");
        }

        public async Task Update(InsuredDto employee)
        {
            await httpClient.PutAsJsonAsync<InsuredDto>("Insured", employee);
        }
    }
}
