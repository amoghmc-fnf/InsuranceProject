using AdminDbService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    public interface IEmailRecordController
    {
        Task<IActionResult> Add(EmailRecordDto admin);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(EmailRecordDto admin);
    }

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailRecordController : ControllerBase, IEmailRecordController
    {
        private readonly IEmailRecordService service;
        public EmailRecordController(IEmailRecordService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<EmailRecordDto> admins = await service.GetAll();
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
        public async Task<IActionResult> Add(EmailRecordDto admin)
        {
                await service.Add(admin);
                return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmailRecordDto admin)
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
