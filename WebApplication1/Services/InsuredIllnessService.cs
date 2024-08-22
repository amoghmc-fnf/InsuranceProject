using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IInsuredIllnessService
    {
        Task Add(InsuredIllnessDto insuredIllnessDto);
        Task Delete(int id);
        Task<List<InsuredIllnessDto>> GetAll();
        Task<InsuredIllnessDto> GetById(int id);
        Task Update(InsuredIllnessDto insuredIllnessDto);
    }

    public class InsuredIllnessService : IInsuredIllnessService
    {
        private readonly FnfProjectContext context;
        public InsuredIllnessService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<InsuredIllnessDto>> GetAll()
        {
            List<InsuredIllnessDto> insuredIllnessDtos = new List<InsuredIllnessDto>();
            await foreach (var insuredIllnessTable in context.InsuredIllnesses)
            {
                InsuredIllnessDto insuredIllnessDto = ConvertToDto(insuredIllnessTable);
                insuredIllnessDtos.Add(insuredIllnessDto);
            }
            return insuredIllnessDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.InsuredIllnesses.FirstOrDefaultAsync((insuredIllnessTable) => insuredIllnessTable.InsuredIllnessId == id);
            if (found != null)
            {
                context.InsuredIllnesses.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(InsuredIllnessDto insuredIllnessDto)
        {
            InsuredIllness insuredIllnessTable = new InsuredIllness();
            ConvertToTable(insuredIllnessDto, insuredIllnessTable);
            context.InsuredIllnesses.Add(insuredIllnessTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(InsuredIllnessDto insuredIllnessDto)
        {
            var found = await context.InsuredIllnesses.FirstOrDefaultAsync((insuredIllnessTable) =>
                insuredIllnessTable.InsuredIllnessId == insuredIllnessDto.InsuredIllnessId);
            if (found != null)
            {
                ConvertToTable(insuredIllnessDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<InsuredIllnessDto> GetById(int id)
        {
            var found = await context.InsuredIllnesses.FirstOrDefaultAsync((insuredIllnessTable) => insuredIllnessTable.InsuredIllnessId == id);
            if (found != null)
            {
                var insuredIllnessDto = ConvertToDto(found);
                return insuredIllnessDto;
            }
            throw new NullReferenceException();
        }

        private InsuredIllnessDto ConvertToDto(InsuredIllness insuredIllnessTable)
        {
            InsuredIllnessDto insuredIllnessDto = new()
            {
                InsuredIllnessId = insuredIllnessTable.InsuredIllnessId,
                IllnessId = insuredIllnessTable.IllnessId,
                InsuredId = insuredIllnessTable.InsuredId,
            };
            return insuredIllnessDto;
        }

        private void ConvertToTable(InsuredIllnessDto insuredIllnessDto, InsuredIllness insuredIllnessTable)
        {
            insuredIllnessTable.InsuredIllnessId = insuredIllnessDto.InsuredIllnessId;
            insuredIllnessTable.IllnessId = insuredIllnessDto.IllnessId;
            insuredIllnessTable.InsuredId = insuredIllnessDto.InsuredId;
            return;
        }
    }
}
