using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    public interface IPolicyController
    {
        Task<IActionResult> Add(PolicyDto policyDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(PolicyDto policyDto);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PolicyContoller : ControllerBase, IPolicyController
    {
        private readonly IPolicyService service;
        public PolicyContoller(IPolicyService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<PolicyDto> policyDtos = await service.GetAll();
            return Ok(policyDtos);
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
        public async Task<IActionResult> Add(PolicyDto policyDto)
        {
            await service.Add(policyDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PolicyDto policyDto)
        {
            try
            {
                await service.Update(policyDto);
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

