using InsuranceApi.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public class AdminDtoService
    {
        private readonly HttpClient httpClient;
        public AdminDtoService(HttpClient httpClient)
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

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Admin/{id}");
        }

        public async Task Update(AdminDto employee)
        {
            await httpClient.PutAsJsonAsync<AdminDto>("Admin", employee);
        }
    }
}
