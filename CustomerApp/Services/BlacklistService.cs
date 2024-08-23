using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IBlacklistDtoService
    {
        Task Add(BlacklistDto employee);
        Task DeleteById(int id);
        Task<List<BlacklistDto>> GetAll();
        Task<BlacklistDto> GetById(int id);
        Task Update(BlacklistDto employee);
    }

    public class BlacklistDtoService : IBlacklistDtoService
    {
        private readonly HttpClient httpClient;
        public BlacklistDtoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<BlacklistDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<BlacklistDto>>("Blacklist");
        }

        public async Task<BlacklistDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<BlacklistDto>($"Blacklist/{id}");
        }

        public async Task Add(BlacklistDto employee)
        {
            await httpClient.PostAsJsonAsync<BlacklistDto>("Blacklist", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Blacklist/{id}");
        }

        public async Task Update(BlacklistDto employee)
        {
            await httpClient.PutAsJsonAsync<BlacklistDto>("Blacklist", employee);
        }
    }
}
