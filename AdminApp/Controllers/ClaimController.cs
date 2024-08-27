using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminApp.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly IEmailRecordService _emailRecordService;
        private readonly IPolicyHolderService _policyHolderService;

        public ClaimController(
            IClaimService claimService, 
            IEmailRecordService emailRecordService,
            IPolicyHolderService policyHolderService)
        {
            _claimService = claimService;
            _emailRecordService = emailRecordService;
            _policyHolderService = policyHolderService;
        }

        public async Task<IActionResult> ManageClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return View(claims);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateClaimStatus(int claimId, String status, decimal dispenseAmount)
        {
            await _claimService.UpdateClaimStatusAsync(claimId, status, dispenseAmount);
            ClaimDto claim = await _claimService.GetById(claimId);
            PolicyHolderDto policyHolder = await _policyHolderService.GetById(claim.PolicyHolderId);
            EmailRecordDto email = new()
            {
                FromEmail = "support@promedico.com",
                ToEmail = policyHolder.Email,
                Subject = "Status Update",
                Content = $"Your claim status has changed to {status}",
            };
            _emailRecordService.Add(email);
            return RedirectToAction("ManageClaims");
        }

    }
}