using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IHospitalService
    {
        Task Add(HospitalDto hospitalDto);
        Task Delete(int id);
        Task<List<HospitalDto>> GetAll();
        Task<HospitalDto> GetById(int id);
        Task Update(HospitalDto hospitalDto);
    }

    public class HospitalService : IHospitalService
    {
        private readonly FnfProjectContext context;
        public HospitalService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<HospitalDto>> GetAll()
        {
            List<HospitalDto> hospitalDtos = new List<HospitalDto>();
            await foreach (var hospitalTable in context.Hospitals)
            {
                HospitalDto hospitalDto = ConvertToDto(hospitalTable);
                hospitalDtos.Add(hospitalDto);
            }
            return hospitalDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Hospitals.FirstOrDefaultAsync((hospitalTable) => hospitalTable.HospitalId == id);
            if (found != null)
            {
                context.Hospitals.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(HospitalDto hospitalDto)
        {
            Hospital hospitalTable = new Hospital();
            ConvertToTable(hospitalDto, hospitalTable);
            context.Hospitals.Add(hospitalTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(HospitalDto hospitalDto)
        {
            var found = await context.Hospitals.FirstOrDefaultAsync((hospitalTable) =>
                hospitalTable.HospitalId == hospitalDto.HospitalId);
            if (found != null)
            {
                ConvertToTable(hospitalDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<HospitalDto> GetById(int id)
        {
            var found = await context.Hospitals.FirstOrDefaultAsync((hospitalTable) => hospitalTable.HospitalId == id);
            if (found != null)
            {
                var hospitalDto = ConvertToDto(found);
                return hospitalDto;
            }
            throw new NullReferenceException();
        }

        private HospitalDto ConvertToDto(Hospital hospitalTable)
        {
            HospitalDto hospitalDto = new()
            {
                HospitalId = hospitalTable.HospitalId,
                Name = hospitalTable.Name,
                Address = hospitalTable.Address,
                Phone = hospitalTable.Phone,
            };
            return hospitalDto;
        }

        private void ConvertToTable(HospitalDto hospitalDto, Hospital hospitalTable)
        {
            hospitalTable.HospitalId = hospitalDto.HospitalId;
            hospitalTable.Name = hospitalDto.Name;
            hospitalTable.Address = hospitalDto.Address;
            hospitalTable.Phone = hospitalDto.Phone;
            return;
        }
    }
}
