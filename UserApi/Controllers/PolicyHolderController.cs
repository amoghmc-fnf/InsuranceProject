using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyClientAppApi.Data;
using UserDbService.Services;

namespace UserApi.Controllers
{
    public interface IPolicyHolderController
    {
        Task<IActionResult> Add(PolicyHolderDto policyHolderDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(PolicyHolderDto policyHolderDto);
        Task<IActionResult> UpdateStatus(int id, [FromBody] int status);

        
        Task<ActionResult<PolicyHolderDto>> GetPolicyHolderByEmail(string email);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHolderController : ControllerBase, IPolicyHolderController
    {
        private readonly IPolicyHolderService service;
        public PolicyHolderController(IPolicyHolderService service)
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

        [HttpPut]
        [Route("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] int status)
        {
            try
            {
                await service.UpdateStatus(id, status);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet("getByEmail")]
        public async Task<ActionResult<PolicyHolderDto>> GetPolicyHolderByEmail(string email)
        {
            var policyHolder = await service.GetPolicyHolderByEmailAsync(email);

            if (policyHolder == null)
            {
                return NotFound("Policy holder not found with the given email.");
            }

            return Ok(policyHolder);
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginModel)
        {
            var user = await service.ValidateUser(loginModel.Email, loginModel.PasswordHash);

            if (user == null)
            {
                // Optional: Generate a token if you're using JWT
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}

