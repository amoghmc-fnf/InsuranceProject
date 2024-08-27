using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IIllnessService
    {
        Task Add(IllnessDto employee);
        Task DeleteById(int id);
        Task<List<IllnessDto>> GetAll();
        Task<IllnessDto> GetById(int id);
        Task Update(IllnessDto employee);
    }

    public class IllnessService : IIllnessService
    {
        private readonly HttpClient httpClient;
        public IllnessService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<IllnessDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<IllnessDto>>("Illness");
        }

        public async Task<IllnessDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<IllnessDto>($"Illness/{id}");
        }

        public async Task Add(IllnessDto employee)
        {
            await httpClient.PostAsJsonAsync<IllnessDto>("Illness", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Illness/{id}");
        }

        public async Task Update(IllnessDto employee)
        {
            await httpClient.PutAsJsonAsync<IllnessDto>("Illness", employee);
        }
    }
}
