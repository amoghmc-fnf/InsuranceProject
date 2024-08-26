using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AdminApp.Models;

namespace AdminApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;

        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Admin> GetAdminByIdAsync(int adminId)
        {
            var response = await _httpClient.GetAsync("Admin");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var admins = JsonSerializer.Deserialize<List<Admin>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var admin = admins?.FirstOrDefault(a => a.AdminId == adminId);
            return admin;
        }

        public async Task<Admin> GetAdminByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync("Admin");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var adminList = JsonSerializer.Deserialize<List<Admin>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var admin = adminList?.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return admin;
            }
            return null;
        }


        public Task Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }

    public interface IAdminService
    {
        Task<Admin> GetAdminByIdAsync(int adminId);
        Task<Admin> GetAdminByNameAsync(string name);
        Task Authenticate(string username, string password);
    }
}