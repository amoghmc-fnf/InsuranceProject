using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IInsuranceTypeService
    {
        Task Add(InsuranceTypeDto insuranceTypeDto);
        Task Delete(int id);
        Task<List<InsuranceTypeDto>> GetAll();
        Task<InsuranceTypeDto> GetById(int id);
        Task Update(InsuranceTypeDto insuranceTypeDto);
    }

    public class InsuranceTypeService : IInsuranceTypeService
    {
        private readonly FnfProjectContext context;

        public InsuranceTypeService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<InsuranceTypeDto>> GetAll()
        {
            List<InsuranceTypeDto> insuranceTypeDtos = [];
            await foreach (var insuranceTypeTable in context.InsuranceTypes)
            {
                var insuranceTypeDto = ConvertToDto(insuranceTypeTable);
                insuranceTypeDtos.Add(insuranceTypeDto);
            }
            return insuranceTypeDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.InsuranceTypes.FirstOrDefaultAsync((insuranceTypeTable) =>
                insuranceTypeTable.InsuranceId == id);
            if (found != null)
            {
                context.InsuranceTypes.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(InsuranceTypeDto insuranceTypeDto)
        {
            InsuranceType insuranceTypeTable = new();
            ConvertToTable(insuranceTypeDto, insuranceTypeTable);
            context.InsuranceTypes.Add(insuranceTypeTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(InsuranceTypeDto insuranceTypeDto)
        {
            var found = await context.InsuranceTypes.FirstOrDefaultAsync((insuranceTypeTable) =>
                insuranceTypeTable.InsuranceId == insuranceTypeDto.InsuranceId);
            if (found != null)
            {
                ConvertToTable(insuranceTypeDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<InsuranceTypeDto> GetById(int id)
        {
            var found = await context.InsuranceTypes.FirstOrDefaultAsync((insuranceTypeTable) =>
                insuranceTypeTable.InsuranceId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private InsuranceTypeDto ConvertToDto(InsuranceType insuranceTypeTable)
        {
            InsuranceTypeDto insuranceTypeDto = new()
            {
                InsuranceId = insuranceTypeTable.InsuranceId,
                InsuranceType = insuranceTypeTable.Name,
                Description = insuranceTypeTable.Description,
                BaseRate = insuranceTypeTable.BaseRate,
                CoverageSize = insuranceTypeTable.CoverageSize,
            };
            return insuranceTypeDto;
        }

        private void ConvertToTable(InsuranceTypeDto insuranceTypeDto, InsuranceType insuranceTypeTable)
        {
            insuranceTypeTable.InsuranceId = insuranceTypeDto.InsuranceId;
            insuranceTypeTable.Name = insuranceTypeDto.InsuranceType;
            insuranceTypeTable.Description = insuranceTypeDto.Description;
            insuranceTypeTable.BaseRate = insuranceTypeDto.BaseRate;
            insuranceTypeTable.CoverageSize = insuranceTypeDto.CoverageSize;
            return;
        }
    }
}
