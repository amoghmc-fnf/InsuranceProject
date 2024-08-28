﻿using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AdminApp.Models;
using JwtModels;
using AdminApp.Services;
using Newtonsoft.Json;

namespace AdminApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public AdminService(HttpClient httpClient, IAuthService authService) /*IAuthService authService)*/
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task AddTokenJwtTokenHeader()
        {
            var result = await _authService.GetJwtToken(new UserLogin { key = "secretPassword" });
            var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(result);
            Console.WriteLine("1" + tokenInfo.Token);
            Console.WriteLine("2" + tokenInfo);
            Console.WriteLine("3" + result);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", tokenInfo.Token);
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