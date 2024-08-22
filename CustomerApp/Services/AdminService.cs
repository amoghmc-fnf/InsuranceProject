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
            return await httpClient.GetFromJsonAsync<List<AdminDto>>("AdminDto");
        }

        public async Task<AdminDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<AdminDto>($"AdminDto/{id}");
        }

        public async Task Add(AdminDto employee)
        {
            await httpClient.PostAsJsonAsync<AdminDto>("AdminDto", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"AdminDto/{id}");
        }

        public async Task Update(AdminDto employee)
        {
            await httpClient.PutAsJsonAsync<AdminDto>("AdminDto", employee);
        }
    }
}
