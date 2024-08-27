using System.Net.Http;
using System.Net.Http.Json;

namespace AdminApp.Services
{
    public interface IPaymentService
    {
        Task Add(PaymentDto employee);
        Task DeleteById(int id);
        Task<List<PaymentDto>> GetAll();
        Task<PaymentDto> GetById(int id);
        Task<PaymentDto> GetPaymentsByInsuredPolicyIdAsync(int insuredPolicyId);
        Task Update(PaymentDto employee);
    }

    public class PaymentService : IPaymentService
    {
        private readonly HttpClient httpClient;
        public PaymentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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

        public async Task DeleteById(int id)
        {
            await httpClient.DeleteAsync($"Payment/{id}");
        }

        public async Task Update(PaymentDto employee)
        {
            await httpClient.PutAsJsonAsync<PaymentDto>("Payment", employee);
        }

        public async Task<PaymentDto> GetPaymentsByInsuredPolicyIdAsync(int insuredPolicyId)
        {
            var response = await this.GetAll();
            var payment = new PaymentDto();

            foreach (var item in response)
            {
                if (item.InsuredPolicyId == insuredPolicyId)
                {
                    payment.PolicyHolderId = item.PolicyHolderId;
                    payment.InsuredPolicyId = item.InsuredPolicyId;
                    payment.PaymentDate = item.PaymentDate;
                    payment.PaymentId = item.PaymentId;
                    payment.PaymentAmount = item.PaymentAmount;
                }
            }
            return payment;

        }
    }
}
