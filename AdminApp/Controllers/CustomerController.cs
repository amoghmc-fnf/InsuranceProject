using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AdminApp.Services;

namespace AdminApp.Controllers
{
    [EnableCors("AllowSpecificOrigin")]  // Ensure CORS policy is applied
    public class CustomerController : Controller
    {
        private readonly IPolicyHolderService _policyHolderService;
        private readonly IEmailRecordService _emailRecordService;

        public CustomerController(IPolicyHolderService policyHolderService, IEmailRecordService emailRecordService)
        {
            _policyHolderService = policyHolderService;
            _emailRecordService = emailRecordService;
        }

        public async Task<IActionResult> ManageCustomers()
        {
            var policyHolders = await _policyHolderService.GetPolicyHoldersAsync();
            return View(policyHolders);
        }

        [HttpPut]
        [Route("PolicyHolder/{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] int status)
        {
            try
            {
                await _policyHolderService.UpdateStatusAsync(id, status);
                PolicyHolderDto policyHolder = await _policyHolderService.GetById(id);
                string update;
                if (status == 1)
                {
                    update = "Active";
                }
                else
                {
                    update = "Blocked";
                }
                EmailRecordDto email = new()
                {
                    FromEmail = "admin@promedico.com",
                    ToEmail = policyHolder.Email,
                    Subject = "Account Status Updated",
                    Content = $"Your account status has been updated to {update}. Please contact admin for more details."
                };
                _emailRecordService.Add(email);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the status.");
            }
        }
    }
}