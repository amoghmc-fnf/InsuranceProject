using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Service
{
    public interface IAdminService
    {
        Task Add(AdminDto admin);
        Task Delete(int id);
        Task<List<AdminDto>> GetAll();
        Task<AdminDto> GetById(int id);
        Task Update(AdminDto admin);
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
            List<AdminDto> admins = new List<AdminDto>();
            await foreach (var admin in context.Admins)
            {
                AdminDto adminDto = new AdminDto();
                adminDto.AdminId = admin.AdminId;
                adminDto.Name = admin.Name;
                adminDto.PasswordHash = admin.PasswordHash;
                admins.Add(adminDto);
            }
            return admins;
        }

        public async Task Delete(int id)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adm) => adm.AdminId == id);
            if (found != null)
            {
                context.Admins.Remove(found);
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task Add(AdminDto admin)
        {
            Admin adminTable = new Admin();
            adminTable.Name = admin.Name;
            adminTable.PasswordHash = admin.PasswordHash;
            context.Admins.Add(adminTable);
            await context.SaveChangesAsync();
            return;
        }

        public async Task Update(AdminDto admin)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adm) => adm.AdminId == admin.AdminId);
            if (found != null)
            {
                found.Name = admin.Name;
                found.PasswordHash = admin.PasswordHash;
                await context.SaveChangesAsync();
                return;
            }
            throw new NullReferenceException();
        }

        public async Task<AdminDto> GetById(int id)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adm) => adm.AdminId == id);
            if (found != null)
            {
                AdminDto admin = new AdminDto()
                {
                    AdminId = found.AdminId,
                    Name = found.Name,
                    PasswordHash = found.PasswordHash
                };
                return admin;
            }
            throw new NullReferenceException();
        }
    }
}
