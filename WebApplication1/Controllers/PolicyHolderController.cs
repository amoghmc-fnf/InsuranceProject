using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    public interface IPolicyHolderController
    {
        Task<IActionResult> Add(PolicyHolderDto policyHolderDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(PolicyHolderDto policyHolderDto);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHolderContoller : ControllerBase, IPolicyHolderController
    {
        private readonly IPolicyHolderService service;
        public PolicyHolderContoller(IPolicyHolderService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<PolicyHolderDto> policyHolderDtos = await service.GetAll();
            return Ok(policyHolderDtos);
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
        public async Task<IActionResult> Add(PolicyHolderDto policyHolderDto)
        {
            await service.Add(policyHolderDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PolicyHolderDto policyHolderDto)
        {
            try
            {
                await service.Update(policyHolderDto);
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

