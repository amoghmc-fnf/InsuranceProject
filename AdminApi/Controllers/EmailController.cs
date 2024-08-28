using AdminDbService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    public interface IEmailRecordController
    {
        Task<IActionResult> Add(EmailRecordDto emailRecord);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(EmailRecordDto emailRecord);
    }

    //[Authorize]
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
            List<EmailRecordDto> emailRecords = await service.GetAll();
            return Ok(emailRecords);
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
        public async Task<IActionResult> Add(EmailRecordDto emailRecord)
        {
                await service.Add(emailRecord);
                return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmailRecordDto emailRecord)
        {
            try
            {
                await service.Update(emailRecord);
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
