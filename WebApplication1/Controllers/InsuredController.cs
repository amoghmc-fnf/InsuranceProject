using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    public interface IInsuredController
    {
        Task<IActionResult> Add(InsuredDto insuredDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(InsuredDto insuredDto);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class InsuredContoller : ControllerBase, IInsuredController
    {
        private readonly IInsuredService service;
        public InsuredContoller(IInsuredService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InsuredDto> insuredDtos = await service.GetAll();
            return Ok(insuredDtos);
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
        public async Task<IActionResult> Add(InsuredDto insuredDto)
        {
            await service.Add(insuredDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(InsuredDto insuredDto)
        {
            try
            {
                await service.Update(insuredDto);
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

