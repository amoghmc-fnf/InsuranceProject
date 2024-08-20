using InsuranceApi.Data;
using InsuranceApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Controllers
{
    public interface IAdminController
    {
        Task<IActionResult> Add(AdminDto admin);
        Task<IActionResult> Delete(AdminDto admin);
        Task<IActionResult> GetAllAdmins();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Put(AdminDto admin);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase, IAdminController
    {
        private FnfProjectContext context;
        public AdminController(FnfProjectContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
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
            return Ok(admins);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(AdminDto admin)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adm) => adm.AdminId == admin.AdminId);
            if (found != null)
            {
                context.Admins.Remove(found);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminDto admin)
        {
            Admin adminTable = new Admin();
            adminTable.Name = admin.Name;
            adminTable.PasswordHash = admin.PasswordHash;
            context.Admins.Add(adminTable);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(AdminDto admin)
        {
            var found = await context.Admins.FirstOrDefaultAsync((adm) => adm.AdminId == admin.AdminId);
            if (found != null)
            {
                found.Name = admin.Name;
                found.PasswordHash = admin.PasswordHash;
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
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
                return Ok(admin);
            }
            return NotFound();
        }
    }
}
