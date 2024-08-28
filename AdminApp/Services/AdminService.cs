using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AdminApp.Models;
using JwtModels;
using AdminApp.Services;

namespace AdminApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        private string token;

        public AdminService(HttpClient httpClient, IAuthService authService) /*IAuthService authService)*/
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task AddTokenJwtTokenHeader()
        {
            if (token is null)
            {
                token = await _authService.GetJwtToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
        }

        public async Task<Admin> GetAdminByIdAsync(int adminId)
        {
            await this.AddTokenJwtTokenHeader();
            var response = await _httpClient.GetAsync("Admin");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var admins = System.Text.Json.JsonSerializer.Deserialize<List<Admin>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var admin = admins?.FirstOrDefault(a => a.AdminId == adminId);
            return admin;
        }

        public async Task<Admin> GetAdminByNameAsync(string name)
        {
            await this.AddTokenJwtTokenHeader();
            var response = await _httpClient.GetAsync("Admin");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var adminList = System.Text.Json.JsonSerializer.Deserialize<List<Admin>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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