using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IIllnessService
    {
        Task Add(IllnessDto illnessDto);
        Task Delete(int id);
        Task<List<IllnessDto>> GetAll();
        Task<IllnessDto> GetById(int id);
        Task Update(IllnessDto illnessDto);
    }

    public class IllnessService : IIllnessService
    {
        private readonly FnfProjectContext context;

        public IllnessService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<IllnessDto>> GetAll()
        {
            List<IllnessDto> illnessDtos = [];
            await foreach (var illnessTable in context.Illnesses)
            {
                var illnessDto = ConvertToDto(illnessTable);
                illnessDtos.Add(illnessDto);
            }
            return illnessDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Illnesses.FirstOrDefaultAsync((illnessTable) =>
                illnessTable.IllnessId == id);
            if (found != null)
            {
                context.Illnesses.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(IllnessDto illnessDto)
        {
            Illness illnessTable = new();
            ConvertToTable(illnessDto, illnessTable);
            context.Illnesses.Add(illnessTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(IllnessDto illnessDto)
        {
            var found = await context.Illnesses.FirstOrDefaultAsync((illnessTable) =>
                illnessTable.IllnessId == illnessDto.IllnessId);
            if (found != null)
            {
                ConvertToTable(illnessDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<IllnessDto> GetById(int id)
        {
            var found = await context.Illnesses.FirstOrDefaultAsync((illnessTable) =>
                illnessTable.IllnessId == id);

            if (found != null)
                return ConvertToDto(found);

            throw new NullReferenceException();
        }

        private IllnessDto ConvertToDto(Illness illnessTable)
        {
            IllnessDto illnessDto = new()
            {
                IllnessId = illnessTable.IllnessId,
                Name = illnessTable.Name,
                Description = illnessTable.Description,
            };
            return illnessDto;
        }

        private void ConvertToTable(IllnessDto illnessDto, Illness illnessTable)
        {
            illnessTable.IllnessId = illnessDto.IllnessId;
            illnessTable.Name = illnessDto.Name;
            illnessTable.Description = illnessDto.Description;
            return;
        }
    }
}
