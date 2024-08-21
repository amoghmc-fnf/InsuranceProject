using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Services
{
    public interface IAdminService
    {
        Task Add(AdminDto adminDto);
        Task Delete(int id);
        Task<List<AdminDto>> GetAll();
        Task<AdminDto> GetById(int id);
        Task Update(AdminDto adminDto);
    }

    public class AdminService : IAdminService
    {
        private readonly FnfProjectContext context;
        public AdminService(FnfProjectContext context)
        {
            this.context = context;
        }

        public async Task<List<AdminDto>> GetAll()
        {
            List<AdminDto> adminDtos = new List<AdminDto>();
            await foreach (var adminTable in context.Admins)
            {
                AdminDto adminDto = ConvertToDto(adminTable);
                adminDtos.Add(adminDto);
            }
            return adminDtos;
        }

        public async Task Delete(int id)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adminTable) => adminTable.AdminId == id);
            if (found != null)
            {
                context.Admins.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(AdminDto adminDto)
        {
            Admin adminTable = new Admin();
            ConvertToTable(adminDto, adminTable);
            context.Admins.Add(adminTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(AdminDto adminDto)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adminTable) =>
                adminTable.AdminId == adminDto.AdminId);
            if (found != null)
            {
                ConvertToTable(adminDto, found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<AdminDto> GetById(int id)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adminTable) => adminTable.AdminId == id);
            if (found != null)
            {
                var adminDto = ConvertToDto(found);
                return adminDto;
            }
            throw new NullReferenceException();
        }

        private AdminDto ConvertToDto(Admin adminTable)
        {
            AdminDto adminDto = new()
            {
                AdminId = adminTable.AdminId,
                Name = adminTable.Name,
                PasswordHash = adminTable.PasswordHash,
            };
            return adminDto;
        }

        private void ConvertToTable(AdminDto adminDto, Admin adminTable)
        {
            adminTable.AdminId = adminDto.AdminId;
            adminTable.Name = adminDto.Name;
            adminTable.PasswordHash = adminDto.PasswordHash;
            return;
        }
    }
}
