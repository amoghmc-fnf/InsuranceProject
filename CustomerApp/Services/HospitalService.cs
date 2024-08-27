using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IHospitalService
    {
        Task Add(HospitalDto employee);
        Task DeleteById(int id);
        Task<List<HospitalDto>> GetAll();
        Task<HospitalDto> GetById(int id);
        Task Update(HospitalDto employee);
    }

    public class HospitalService : IHospitalService
    {
        private readonly HttpClient httpClient;
        public HospitalService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<HospitalDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<HospitalDto>>("Hospital");
        }

        public async Task<HospitalDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<HospitalDto>($"Hospital/{id}");
        }

        public async Task Add(HospitalDto employee)
        {
            await httpClient.PostAsJsonAsync<HospitalDto>("Hospital", employee);
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Hospital/{id}");
        }

        public async Task Update(HospitalDto employee)
        {
            await httpClient.PutAsJsonAsync<HospitalDto>("Hospital", employee);
        }
    }
}
