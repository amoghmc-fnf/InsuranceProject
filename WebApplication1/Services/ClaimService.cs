using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IClaimService
    {
        Task Add(ClaimDto claimDto);
        Task Delete(int id);
        Task<List<ClaimDto>> GetAll();
        Task<ClaimDto> GetById(int id);
        Task Update(ClaimDto claimDto);
    }

    public class ClaimService : IClaimService
    {
        private readonly FnfProjectContext context;

        public ClaimService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<ClaimDto>> GetAll()
        {
            List<ClaimDto> claimDtos = [];
            await foreach (var claimTable in context.Claims)
            {
                var claimDto = ConvertToDto(claimTable);
                claimDtos.Add(claimDto);
            }
            return claimDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Claims.FirstOrDefaultAsync((claimTable) =>
                claimTable.ClaimId == id);
            if (found != null)
            {
                context.Claims.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(ClaimDto claimDto)
        {
            Claim claimTable = new();
            ConvertToTable(claimDto, claimTable);
            context.Claims.Add(claimTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(ClaimDto claimDto)
        {
            var found = await context.Claims.FirstOrDefaultAsync((claimTable) =>
                claimTable.ClaimId == claimDto.ClaimId);
            if (found != null)
            {
                ConvertToTable(claimDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<ClaimDto> GetById(int id)
        {
            var found = await context.Claims.FirstOrDefaultAsync((claimTable) =>
                claimTable.ClaimId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private ClaimDto ConvertToDto(Claim claimTable)
        {
            ClaimDto claimDto = new()
            {
                ClaimId = claimTable.ClaimId,
                PolicyHolderId = claimTable.PolicyHolderId,
                InsuredPolicyId = claimTable.InsuredPolicyId,
                ClaimAmount = claimTable.ClaimAmount,
                ClaimStatus = claimTable.ClaimStatus,
                DispenseAmount = claimTable.DispenseAmount,
                DocumentPath = claimTable.DocumentPath,
                DocumentType = claimTable.DocumentType,
                HospitalId = claimTable.HospitalId,
                ClaimDate = DateTime.Parse(claimTable.ClaimDate.ToString()),
            };
            return claimDto;
        }

        private void ConvertToTable(ClaimDto claimDto, Claim claimTable)
        {
            claimTable.ClaimId = claimDto.ClaimId;
            claimTable.PolicyHolderId = claimDto.PolicyHolderId;
            claimTable.InsuredPolicyId = claimDto.InsuredPolicyId;
            claimTable.ClaimAmount = claimDto.ClaimAmount;
            claimTable.ClaimStatus = claimDto.ClaimStatus;
            claimTable.DispenseAmount = claimDto.DispenseAmount;
            claimTable.DocumentPath = claimDto.DocumentPath;
            claimTable.DocumentType = claimDto.DocumentType;
            claimTable.ClaimDate = DateOnly.FromDateTime(claimDto.ClaimDate);
            claimTable.HospitalId = claimDto.HospitalId;
            return;
        }
    }
}
