using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IInsuredService
    {
        Task Add(InsuredDto insuredDto);
        Task Delete(int id);
        Task<List<InsuredDto>> GetAll();
        Task<InsuredDto> GetById(int id);
        Task Update(InsuredDto insuredDto);
    }

    public class InsuredService : IInsuredService
    {
        private readonly FnfProjectContext context;

        public InsuredService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<InsuredDto>> GetAll()
        {
            List<InsuredDto> insuredDtos = [];
            await foreach (var insuredTable in context.Insureds)
            {
                var insuredDto = ConvertToDto(insuredTable);
                insuredDtos.Add(insuredDto);
            }
            return insuredDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Insureds.FirstOrDefaultAsync((insuredTable) =>
                insuredTable.InsuredId == id);
            if (found != null)
            {
                context.Insureds.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(InsuredDto insuredDto)
        {
            Insured insuredTable = new();
            ConvertToTable(insuredDto, insuredTable);
            context.Insureds.Add(insuredTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(InsuredDto insuredDto)
        {
            var found = await context.Insureds.FirstOrDefaultAsync((insuredTable) =>
                insuredTable.InsuredId == insuredDto.InsuredId);
            if (found != null)
            {
                ConvertToTable(insuredDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<InsuredDto> GetById(int id)
        {
            var found = await context.Insureds.FirstOrDefaultAsync((insuredTable) =>
                insuredTable.InsuredId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private InsuredDto ConvertToDto(Insured insuredTable)
        {
            InsuredDto insuredDto = new()
            {
                InsuredId = insuredTable.InsuredId,
                PolicyHolderId = insuredTable.PolicyHolderId,
                Name = insuredTable.Name,
                Gender = insuredTable.Gender,
                Dob = DateTime.Parse(insuredTable.Dob.ToString()),
                RegistrationDate = DateTime.Parse(insuredTable.RegistrationDate.ToString()),
            };
            return insuredDto;
        }

        private void ConvertToTable(InsuredDto insuredDto, Insured insuredTable)
        {
            insuredTable.InsuredId = insuredDto.InsuredId;
            insuredTable.PolicyHolderId = insuredDto.PolicyHolderId;
            insuredTable.Name = insuredDto.Name;
            insuredTable.Gender = insuredDto.Gender;
            insuredTable.Dob = DateOnly.FromDateTime(insuredDto.Dob);
            insuredTable.RegistrationDate = DateOnly.FromDateTime(insuredDto.RegistrationDate);
            return;
        }
    }
}
