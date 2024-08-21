using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IPolicyService
    {
        Task Add(PolicyDto policyDto);
        Task Delete(int id);
        Task<List<PolicyDto>> GetAll();
        Task<PolicyDto> GetById(int id);
        Task Update(PolicyDto policyDto);
    }

    public class PolicyService : IPolicyService
    {
        private readonly FnfProjectContext context;

        public PolicyService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<PolicyDto>> GetAll()
        {
            List<PolicyDto> policyDtos = [];
            await foreach (var policyTable in context.Policies)
            {
                var policyDto = ConvertToDto(policyTable);
                policyDtos.Add(policyDto);
            }
            return policyDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Policies.FirstOrDefaultAsync((policyTable) =>
                policyTable.PolicyId == id);
            if (found != null)
            {
                context.Policies.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(PolicyDto policyDto)
        {
            Policy policyTable = new();
            ConvertToTable(policyDto, policyTable);
            context.Policies.Add(policyTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(PolicyDto policyDto)
        {
            var found = await context.Policies.FirstOrDefaultAsync((policyTable) =>
                policyTable.PolicyId == policyDto.PolicyId);
            if (found != null)
            {
                ConvertToTable(policyDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<PolicyDto> GetById(int id)
        {
            var found = await context.Policies.FirstOrDefaultAsync((policyTable) =>
                policyTable.PolicyId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private PolicyDto ConvertToDto(Policy policyTable)
        {
            PolicyDto policyDto = new()
            {
                PolicyId = policyTable.PolicyId,
                PolicyNumber = policyTable.PolicyNumber,
                PremiumAmount = policyTable.PremiumAmount,
                InsuranceTypeId = policyTable.InsuranceTypeId,
                StartDate = DateTime.Parse(policyTable.StartDate.ToString()),
                EndDate = DateTime.Parse(policyTable.EndDate.ToString()),
            };
            return policyDto;
        }

        private void ConvertToTable(PolicyDto policyDto, Policy policyTable)
        {
            policyTable.PolicyId = policyDto.PolicyId;
            policyTable.PolicyNumber = policyDto.PolicyNumber;
            policyTable.PremiumAmount = policyDto.PremiumAmount;
            policyTable.InsuranceTypeId = policyDto.InsuranceTypeId;
            policyTable.StartDate = DateOnly.FromDateTime(policyDto.StartDate);
            policyTable.EndDate = DateOnly.FromDateTime(policyDto.EndDate);
            return;
        }
    }
}
