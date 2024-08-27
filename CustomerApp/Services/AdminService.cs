using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IAdminService
    {
        Task Add(AdminDto employee);
        Task Delete(int id);
        Task<List<AdminDto>> GetAll();
        Task<AdminDto> GetById(int id);
        Task Update(AdminDto employee);
    }

    public class AdminService : IAdminService
    {
        private readonly HttpClient httpClient;
        public AdminService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<AdminDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<AdminDto>>("Admin");
        }

        public async Task<AdminDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<AdminDto>($"Admin/{id}");
        }

        public async Task Add(AdminDto employee)
        {
            await httpClient.PostAsJsonAsync<AdminDto>("Admin", employee);
        }

        public async Task Delete(int id)
        {
            await httpClient.DeleteAsync($"Admin/{id}");
        }

        public async Task Update(AdminDto employee)
        {
            await httpClient.PutAsJsonAsync<AdminDto>("Admin", employee);
        }
    }
}
