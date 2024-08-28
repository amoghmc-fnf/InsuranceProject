using System.Net.Http;
using System.Net.Http.Json;
using MyClientApp.Client;

namespace MyClientApp.Services
{
    public interface IPaymentService
    {
        Task Add(PaymentDto employee);
        Task<PaymentDto> AddAndGet(PaymentDto employee);
        Task DeleteById(int id);
        Task<List<PaymentDto>> GetAll();
        Task<PaymentDto> GetById(int id);
        Task Update(PaymentDto employee);
    }

    public class PaymentService : IPaymentService
    {
        private readonly HttpClient httpClient;
        public PaymentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(Program.Configuration["AdminApiUrl"]);
        }

        public async Task<List<PaymentDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<PaymentDto>>("Payment");
        }

        public async Task<PaymentDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<PaymentDto>($"Payment/{id}");
        }

        public async Task Add(PaymentDto employee)
        {
            await httpClient.PostAsJsonAsync<PaymentDto>("Payment", employee);
        }

        public async Task<PaymentDto> AddAndGet(PaymentDto employee)
        {
            var response = await httpClient.PostAsJsonAsync<PaymentDto>("Payment", employee);
            return await response.Content.ReadFromJsonAsync<PaymentDto>();
        }

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Payment/{id}");
        }

        public async Task Update(PaymentDto employee)
        {
            await httpClient.PutAsJsonAsync<PaymentDto>("Payment", employee);
        }
    }
}
