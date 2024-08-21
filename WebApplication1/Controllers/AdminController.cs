using InsuranceApi.Data;
using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Controllers
{
    public interface IAdminController
    {
        Task<IActionResult> Add(AdminDto admin);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(AdminDto admin);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase, IAdminController
    {
        private readonly IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AdminDto> admins = await service.GetAll();
            return Ok(admins);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.Delete(id);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminDto admin)
        {
                await service.Add(admin);
                return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(AdminDto admin)
        {
            try
            {
                await service.Update(admin);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var found = await service.GetById(id);
                return Ok(found);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
