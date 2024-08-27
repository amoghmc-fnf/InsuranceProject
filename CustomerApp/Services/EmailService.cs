using System.Net.Http;
using System.Net.Http.Json;

namespace CustomerApp.Services
{
    public interface IEmailRecordService
    {
        Task Add(EmailRecordDto employee);
        Task Delete(int id);
        Task<List<EmailRecordDto>> GetAll();
        Task<EmailRecordDto> GetById(int id);
        Task Update(EmailRecordDto employee);
    }

    public class EmailRecordService : IEmailRecordService
    {
        private readonly HttpClient httpClient;
        public EmailRecordService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<EmailRecordDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<EmailRecordDto>>("EmailRecord");
        }

        public async Task<EmailRecordDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<EmailRecordDto>($"EmailRecord/{id}");
        }

        public async Task Add(EmailRecordDto employee)
        {
            await httpClient.PostAsJsonAsync<EmailRecordDto>("EmailRecord", employee);
        }

        public async Task Delete(int id)
        {
            await httpClient.DeleteAsync($"EmailRecord/{id}");
        }

        public async Task Update(EmailRecordDto employee)
        {
            await httpClient.PutAsJsonAsync<EmailRecordDto>("EmailRecord", employee);
        }
    }
}
