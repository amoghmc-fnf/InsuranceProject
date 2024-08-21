using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    public interface IInsuranceTypeController
    {
        Task<IActionResult> Add(InsuranceTypeDto insuranceTypeDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(InsuranceTypeDto insuranceTypeDto);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceTypeContoller : ControllerBase, IInsuranceTypeController
    {
        private readonly IInsuranceTypeService service;
        public InsuranceTypeContoller(IInsuranceTypeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InsuranceTypeDto> insuranceTypeDtos = await service.GetAll();
            return Ok(insuranceTypeDtos);
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
        public async Task<IActionResult> Add(InsuranceTypeDto insuranceTypeDto)
        {
            await service.Add(insuranceTypeDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(InsuranceTypeDto insuranceTypeDto)
        {
            try
            {
                await service.Update(insuranceTypeDto);
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

