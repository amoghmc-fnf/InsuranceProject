using InsuranceApi.Data;
using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Controllers
{
    public interface IInsuredIllnessController
    {
        Task<IActionResult> Add(InsuredIllnessDto admin);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(InsuredIllnessDto admin);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class InsuredIllnessController : ControllerBase, IInsuredIllnessController
    {
        private readonly IInsuredIllnessService service;
        public InsuredIllnessController(IInsuredIllnessService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InsuredIllnessDto> admins = await service.GetAll();
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
        public async Task<IActionResult> Add(InsuredIllnessDto admin)
        {
                await service.Add(admin);
                return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(InsuredIllnessDto admin)
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
