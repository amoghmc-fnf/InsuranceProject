using InsuranceApi.Data;
using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Controllers
{
    public interface IClaimController
    {
        Task<IActionResult> Add(ClaimDto claimDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(ClaimDto claimDto);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase, IClaimController
    {
        private readonly IClaimService service;
        public ClaimController(IClaimService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ClaimDto> claims = await service.GetAll();
            return Ok(claims);
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
        public async Task<IActionResult> Add(ClaimDto claim)
        {
            await service.Add(claim);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClaimDto claim)
        {
            try
            {
                await service.Update(claim);
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
