using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IPolicyHolderService
    {
        Task Add(PolicyHolderDto policyHolderDto);
        Task Delete(int id);
        Task<List<PolicyHolderDto>> GetAll();
        Task<PolicyHolderDto> GetById(int id);
        Task Update(PolicyHolderDto policyHolderDto);
    }

    public class PolicyHolderService : IPolicyHolderService
    {
        private readonly FnfProjectContext context;

        public PolicyHolderService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<PolicyHolderDto>> GetAll()
        {
            List<PolicyHolderDto> policyHolderDtos = [];
            await foreach (var policyHolderTable in context.PolicyHolders)
            {
                var policyHolderDto = ConvertToDto(policyHolderTable);
                policyHolderDtos.Add(policyHolderDto);
            }
            return policyHolderDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.PolicyHolders.FirstOrDefaultAsync((policyHolderTable) =>
                policyHolderTable.PolicyHolderId == id);
            if (found != null)
            {
                context.PolicyHolders.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(PolicyHolderDto policyHolderDto)
        {
            PolicyHolder policyHolderTable = new();
            ConvertToTable(policyHolderDto, policyHolderTable);
            context.PolicyHolders.Add(policyHolderTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(PolicyHolderDto policyHolderDto)
        {
            var found = await context.PolicyHolders.FirstOrDefaultAsync((policyHolderTable) =>
                policyHolderTable.PolicyHolderId == policyHolderDto.PolicyHolderId);
            if (found != null)
            {
                ConvertToTable(policyHolderDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<PolicyHolderDto> GetById(int id)
        {
            var found = await context.PolicyHolders.FirstOrDefaultAsync((policyHolderTable) =>
                policyHolderTable.PolicyHolderId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private PolicyHolderDto ConvertToDto(PolicyHolder policyHolderTable)
        {
            PolicyHolderDto policyHolderDto = new()
            {
                PolicyHolderId = policyHolderTable.PolicyHolderId,
                Name = policyHolderTable.Name,
                Address = policyHolderTable.Address,
                Email = policyHolderTable.Email,
                PasswordHash = policyHolderTable.PasswordHash,
                Phone = policyHolderTable.Phone,
                Status = policyHolderTable.Status,
            };
            return policyHolderDto;
        }

        private void ConvertToTable(PolicyHolderDto policyHolderDto, PolicyHolder policyHolderTable)
        {
            policyHolderTable.PolicyHolderId = policyHolderDto.PolicyHolderId;
            policyHolderTable.Name = policyHolderDto.Name;
            policyHolderTable.Address = policyHolderDto.Address;
            policyHolderTable.Email = policyHolderDto.Email;
            policyHolderTable.PasswordHash = policyHolderDto.PasswordHash;
            policyHolderTable.Phone = policyHolderDto.Phone;
            policyHolderTable.Status = policyHolderDto.Status;
            return;
        }
    }
}
