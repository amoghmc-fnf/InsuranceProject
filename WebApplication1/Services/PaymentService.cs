using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IPaymentService
    {
        Task Add(PaymentDto paymentDto);
        Task Delete(int id);
        Task<List<PaymentDto>> GetAll();
        Task<PaymentDto> GetById(int id);
        Task Update(PaymentDto paymentDto);
    }

    public class PaymentService : IPaymentService
    {
        private readonly FnfProjectContext context;

        public PaymentService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<PaymentDto>> GetAll()
        {
            List<PaymentDto> paymentDtos = [];
            await foreach (var paymentTable in context.Payments)
            {
                var paymentDto = ConvertToDto(paymentTable);
                paymentDtos.Add(paymentDto);
            }
            return paymentDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Payments.FirstOrDefaultAsync((paymentTable) =>
                paymentTable.PaymentId == id);
            if (found != null)
            {
                context.Payments.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(PaymentDto paymentDto)
        {
            Payment paymentTable = new();
            ConvertToTable(paymentDto, paymentTable);
            context.Payments.Add(paymentTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(PaymentDto paymentDto)
        {
            var found = await context.Payments.FirstOrDefaultAsync((paymentTable) =>
                paymentTable.PaymentId == paymentDto.PaymentId);
            if (found != null)
            {
                ConvertToTable(paymentDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<PaymentDto> GetById(int id)
        {
            var found = await context.Payments.FirstOrDefaultAsync((paymentTable) =>
                paymentTable.PaymentId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private PaymentDto ConvertToDto(Payment paymentTable)
        {
            PaymentDto paymentDto = new()
            {
                PaymentId = paymentTable.PaymentId,
                PolicyHolderId = paymentTable.PolicyHolderId,
                InsuredPolicyId = paymentTable.InsuredPolicyId,
                PaymentAmount = paymentTable.PaymentAmount,
                PaymentDate = DateTime.Parse(paymentTable.PaymentDate.ToString())
            };
            return paymentDto;
        }

        private void ConvertToTable(PaymentDto paymentDto, Payment paymentTable)
        {
            paymentTable.PaymentId = paymentDto.PaymentId;
            paymentTable.PolicyHolderId = paymentDto.PolicyHolderId;
            paymentTable.InsuredPolicyId = paymentDto.InsuredPolicyId;
            paymentTable.PaymentAmount = paymentDto.PaymentAmount;
            paymentTable.PaymentDate = DateOnly.FromDateTime(paymentDto.PaymentDate);
            return;
        }
    }
}
