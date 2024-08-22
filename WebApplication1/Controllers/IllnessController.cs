using InsuranceApi.Data;
using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Controllers
{
    public interface IIllnessController
    {
        Task<IActionResult> Add(IllnessDto admin);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(IllnessDto admin);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class IllnessController : ControllerBase, IIllnessController
    {
        private readonly IIllnessService service;
        public IllnessController(IIllnessService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<IllnessDto> admins = await service.GetAll();
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
        public async Task<IActionResult> Add(IllnessDto admin)
        {
                await service.Add(admin);
                return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(IllnessDto admin)
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
