using InsuranceApi.Data;
using InsuranceApi.DTOs;
using InsuranceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Controllers
{
    public interface IBlacklistController
    {
        Task<IActionResult> Add(BlacklistDto blacklistDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(BlacklistDto blacklistDto);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistController : ControllerBase, IBlacklistController
    {
        private readonly IBlacklistService service;
        public BlacklistController(IBlacklistService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<BlacklistDto> blacklists = await service.GetAll();
            return Ok(blacklists);
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
        public async Task<IActionResult> Add(BlacklistDto blacklist)
        {
            await service.Add(blacklist);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BlacklistDto blacklist)
        {
            try
            {
                await service.Update(blacklist);
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
