using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminApp.Services;

namespace AdminApp.Controllers
{
    public class PolicyRequestController : Controller
    {
        private readonly IPolicyRequestService _policyRequestService;
        private readonly IPolicyHolderService _policyHolderService;
        private readonly IPaymentService _paymentService;
        private readonly IInsuredDtoService _insuredDtoService;
        private readonly ILogger<PolicyRequestController> _logger;

        public PolicyRequestController(
            IInsuredDtoService insuredService, 
            IPolicyRequestService policyRequestService, 
            IPolicyHolderService policyHolderService,
            IPaymentService paymentService,
            ILogger<PolicyRequestController> logger)
        {
            _policyRequestService = policyRequestService;
            _insuredDtoService = insuredService;
            _policyHolderService = policyHolderService;
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var insuredPolicies = await _policyRequestService.GetInsuredPoliciesAsync();
            var policyRequests = new List<PolicyRequestViewModel>();

            foreach (var policy in insuredPolicies)
            {
                var insured = await _insuredDtoService.GetById(policy.InsuredId);
                var policyHolder = await _policyHolderService.GetById(insured.PolicyHolderId);
                if (policyHolder != null)
                {
                    policyRequests.Add(new PolicyRequestViewModel
                    {
                        InsuredPolicyId = policy.InsuredPolicyId,
                        InsuredId = policy.InsuredId,
                        PolicyId = policy.PolicyId,
                        PolicyHolderId = policyHolder.PolicyHolderId,
                        Name = policyHolder.Name,
                        ContactNo = policyHolder.Phone,
                        ApprovalStatus = policy.ApprovalStatus,
                        ApprovalDate = policy.ApprovalDate
                    });
                }
                else
                {
                    _logger.LogWarning($"PolicyHolder not found for InsuredId: {policy.InsuredId}");
                }
            }

            return View(policyRequests);
        }

        public async Task<IActionResult> Review(int insuredPolicyId)
        {
            var insuredPolicy = (await _policyRequestService.GetInsuredPoliciesAsync())
                .FirstOrDefault(p => p.InsuredPolicyId == insuredPolicyId);

            if (insuredPolicy == null)
            {
                _logger.LogWarning($"InsuredPolicy not found for Id: {insuredPolicyId}");
                return NotFound();
            }

            var insured = await _insuredDtoService.GetById(insuredPolicy.InsuredId);
            var policyHolder = await _policyHolderService.GetById(insured.PolicyHolderId);

            var policy = await _policyRequestService.GetPolicyAsync(insuredPolicy.PolicyId);
            var payments = await _paymentService.GetPaymentsByInsuredPolicyIdAsync(insuredPolicy.InsuredPolicyId);

            var viewModel = new ReviewPolicyRequestViewModel
            {
                PolicyHolderName = policyHolder?.Name ?? "N/A",
                ContactNo = policyHolder?.Phone ??"N/A",
                InsuredId = insuredPolicy.InsuredId,
                InsuredPolicyId = insuredPolicy.InsuredPolicyId,
                PolicyId = insuredPolicy.PolicyId,
                AdminId = insuredPolicy.AdminId,
                PremiumAmount = policy?.PremiumAmount ?? 0,
                PaymentId = payments?.PaymentId ?? 0,
                PaymentAmount = payments?.PaymentAmount ?? 0,
                PaymentDate = payments?.PaymentDate ?? null
            };

            return PartialView("_ReviewPolicyRequest", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApprovalStatus(int insuredPolicyId, string approvalStatus)
        {
            if (string.IsNullOrWhiteSpace(approvalStatus))
            {
                _logger.LogWarning("Approval status is required.");
                return BadRequest("Approval status is required.");
            }

            var isValidStatus = new[] { "Approved", "Rejected", "Pending" }.Contains(approvalStatus);
            if (!isValidStatus)
            {
                _logger.LogWarning($"Invalid approval status: {approvalStatus}");
                return BadRequest("Invalid approval status.");
            }

            var success = await _policyRequestService.UpdateApprovalStatusAsync(insuredPolicyId, approvalStatus);
            if (success)
            {
                _logger.LogInformation($"Approval status updated to '{approvalStatus}' for InsuredPolicyId: {insuredPolicyId}");
                return Json(new { success = true });
            }
            else
            {
                _logger.LogError($"Failed to update approval status for InsuredPolicyId: {insuredPolicyId}");
                return BadRequest("Failed to update approval status.");
            }
        }
    }

    public class PolicyRequestViewModel
    {
        public int InsuredPolicyId { get; set; }
        public int InsuredId { get; set; }
        public int PolicyId { get; set; }
        public int PolicyHolderId { get; set; }
        public string Name { get; set; }
        public string ApprovalStatus { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ContactNo { get;  set; }
    }

    public class ReviewPolicyRequestViewModel
    {
        public string PolicyHolderName { get; set; }
        public int InsuredId { get; set; }
        public int InsuredPolicyId { get; set; }
        public int PolicyId { get; set; }
        public int AdminId { get; set; }
        public decimal PremiumAmount { get; set; }
        public int PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public object? ContactNo { get; internal set; }
    }
}