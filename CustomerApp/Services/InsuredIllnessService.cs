using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IInsuredIllnessService
    {
        Task Add(InsuredIllnessDto employee);
        Task DeleteById(int id);
        Task<List<InsuredIllnessDto>> GetAll();
        Task<InsuredIllnessDto> GetById(int id);
        Task Update(InsuredIllnessDto employee);
    }

    public class InsuredIllnessService : IInsuredIllnessService
    {
        private readonly HttpClient httpClient;
        public InsuredIllnessService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<InsuredIllnessDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<InsuredIllnessDto>>("InsuredIllness");
        }

        public async Task<InsuredIllnessDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<InsuredIllnessDto>($"InsuredIllness/{id}");
        }

        public async Task Add(InsuredIllnessDto employee)
        {
            await httpClient.PostAsJsonAsync<InsuredIllnessDto>("InsuredIllness", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"InsuredIllness/{id}");
        }

        public async Task Update(InsuredIllnessDto employee)
        {
            await httpClient.PutAsJsonAsync<InsuredIllnessDto>("InsuredIllness", employee);
        }
    }
}
