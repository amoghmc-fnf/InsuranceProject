using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyDbService.Services;

namespace PolicyApi.Controllers
{
    public interface IInsuredPolicyController
    {
        Task<IActionResult> Add(InsuredPolicyDto insuredPolicyDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(InsuredPolicyDto insuredPolicyDto);
        Task<IActionResult> UpdateApprovalStatus(int id, [FromBody] string approvalStatus);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class InsuredPolicyController : ControllerBase, IInsuredPolicyController
    {
        private readonly IInsuredPolicyService service;
        public InsuredPolicyController(IInsuredPolicyService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InsuredPolicyDto> insuredPolicyDtos = await service.GetAll();
            return Ok(insuredPolicyDtos);
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
        public async Task<IActionResult> Add(InsuredPolicyDto insuredPolicyDto)
        {
            InsuredPolicyDto addedInsuredPolicyDto = await service.Add(insuredPolicyDto);
            return Ok(addedInsuredPolicyDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(InsuredPolicyDto insuredPolicyDto)
        {
            try
            {
                await service.Update(insuredPolicyDto);
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

        [HttpPut]
        [Route("{id}/ApprovalStatus")]
        public async Task<IActionResult> UpdateApprovalStatus(int id, [FromBody] string approvalStatus)
        {
            try
            {
                await service.UpdateApprovalStatus(id, approvalStatus);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}

