﻿using System.Net.Http;
using System.Net.Http.Json;

namespace AdminApp.Services
{
    public interface IInsuredDtoService
    {
        Task Add(InsuredDto employee);
        Task DeleteById(int id);
        Task<List<InsuredDto>> GetAll();
        Task<InsuredDto> GetById(int id);
        Task Update(InsuredDto employee);
    }

    public class InsuredDtoService : IInsuredDtoService
    {
        private readonly HttpClient httpClient;
        public InsuredDtoService(HttpClient httpClient)
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
