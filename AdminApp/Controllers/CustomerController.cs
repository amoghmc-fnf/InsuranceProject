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

        public CustomerController(IPolicyHolderService policyHolderService)
        {
            _policyHolderService = policyHolderService;
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
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the status.");
            }
        }
    }
}