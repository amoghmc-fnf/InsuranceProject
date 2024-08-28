using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using iText.Layout.Properties;
using iText.Kernel.Colors;

using iText.IO.Image;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using PolicyDbService.Services;
using UserDbService.Services;
//using Document = iText.Layout.Document;


namespace InsuranceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPolicyHolderService _policyHolderService;
        private readonly IPolicyService _policyService;
        private readonly IInsuredPolicyService _insuredPolicyService;
        private readonly IInsuredService _insuredService;
        private readonly IInsuranceTypeService _insuranceTypeService;

        public PdfController(IPolicyHolderService policyHolderService, IPolicyService policyService, IInsuredPolicyService insuredPolicyService, IInsuredService insuredService, IInsuranceTypeService insuranceTypeService)
        {
            _policyHolderService = policyHolderService;
            _policyService = policyService;
            _insuredPolicyService = insuredPolicyService;
            _insuredService = insuredService;
            _insuranceTypeService = insuranceTypeService;
        }

        [HttpGet("GeneratePolicyPdf")]
        public async Task<IActionResult> GeneratePolicyPdf(int insuredPolicyId)
        {
            try
            {
                var insredPolicy = await _insuredPolicyService.GetById(insuredPolicyId);
                var policyId = insredPolicy.PolicyId;
                var insuredId = insredPolicy.InsuredId;
                var insured = await _insuredService.GetById(insuredId);
                var policyHolderId = insured.PolicyHolderId;
                var policyHolder = await _policyHolderService.GetById(policyHolderId);
                var policy = await _policyService.GetById(policyId);
                var insuredTypeId = policy.InsuranceTypeId;
                var insuranceType = await _insuranceTypeService.GetById(insuredTypeId);


                // Filter insured persons by matching PolicyId
                var insuredPolicies = (await _insuredPolicyService.GetAll())
            .Where(ip => ip.PolicyId == policyId)
            .ToList();

                var insuredIds = insuredPolicies.Select(ip => ip.InsuredId).ToList();
                var insuredMembers = (await _insuredService.GetAll())
                    .Where(i => insuredIds.Contains(i.InsuredId))
                    .ToList();

                if (policyHolder == null)
                {
                    return NotFound("Policy holder not found.");
                }

                if (policy == null)
                {
                    return NotFound("Policy not found.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    var pdfWriter = new PdfWriter(memoryStream);
                    var pdfDocument = new PdfDocument(pdfWriter);
                    var document = new Document(pdfDocument);

                    // Add title
                    document.Add(new Paragraph("Certificate of Insurance").SetBold().SetFontSize(20).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                    document.Add(new Paragraph(" ")); // Spacer

                    // Create table for Policy Information
                    var policyInfoTable = new Table(2);
                    policyInfoTable.AddCell("Policy Number");
                    policyInfoTable.AddCell(policy.PolicyNumber);

                    document.Add(new Paragraph("Policy ").SetBold().SetBackgroundColor(iText.Kernel.Colors.ColorConstants.YELLOW));
                    document.Add(policyInfoTable);

                    document.Add(new Paragraph(" ")); // Spacer

                    // Create table for General Information
                    var generalInfoTable = new Table (2);
                    generalInfoTable.AddCell("Name");
                    generalInfoTable.AddCell(policyHolder.Name);
                    generalInfoTable.AddCell("Emial");
                    generalInfoTable.AddCell(policyHolder.Email);
                    generalInfoTable.AddCell("Contact Number");
                    generalInfoTable.AddCell(policyHolder.Phone);
                    generalInfoTable.AddCell("Residence Address");
                    generalInfoTable.AddCell(policyHolder.Address);

                    document.Add(new Paragraph("Account Holder Information").SetBold().SetBackgroundColor(iText.Kernel.Colors.ColorConstants.YELLOW));
                    document.Add(generalInfoTable);

                    document.Add(new Paragraph(" ")); // Spacer

                    var PolicyTable = new Table(2);
                    PolicyTable.AddCell("Insurance Policy Number");
                    PolicyTable.AddCell(policy.PolicyNumber); // Example Placeholder
                    PolicyTable.AddCell("Insurance Name");
                    PolicyTable.AddCell(insuranceType.InsuranceType);
                    PolicyTable.AddCell("Start Date");
                    PolicyTable.AddCell(policy.StartDate.ToShortDateString()); // Example Placeholder
                    PolicyTable.AddCell("End Date");
                    PolicyTable.AddCell(policy.EndDate.ToShortDateString());
                    PolicyTable.AddCell("Premium Amount");
                    PolicyTable.AddCell("INR " + policy.PremiumAmount.ToString()); // Example Placeholder

                    document.Add(new Paragraph("Insurance Information").SetBold().SetBackgroundColor(iText.Kernel.Colors.ColorConstants.YELLOW));
                    document.Add(PolicyTable);

                    document.Add(new Paragraph(" "));

                    var insuredInfoTable = new Table(2);
                    var space = new Table(1);
                    var Count = 1;
                    foreach (var member in insuredMembers)
                    {
                        // Create table for insured details (as per the example image)
                        insuredInfoTable.AddCell("memeber");
                        insuredInfoTable.AddCell(Count.ToString());
                        insuredInfoTable.AddCell("Insured Name");
                        insuredInfoTable.AddCell(member.Name); // Example Placeholder
                        insuredInfoTable.AddCell("Gender");
                        insuredInfoTable.AddCell(member.Gender); // Example Placeholder
                        insuredInfoTable.AddCell("Date of Birth");
                        insuredInfoTable.AddCell(member.Dob.ToShortDateString()); // Example Placeholder
                        insuredInfoTable.AddCell("Registration Date");
                        insuredInfoTable.AddCell(member.RegistrationDate.ToLongDateString());
                        space.AddCell("");
                        Count++;

                        document.Add(new Paragraph(" "));

                    }
                    document.Add(new Paragraph("Information of the Person  Insured").SetBold().SetBackgroundColor(iText.Kernel.Colors.ColorConstants.YELLOW));
                    document.Add(insuredInfoTable);

                    document.Close();

                    var fileContent = memoryStream.ToArray();

                    // Return the PDF file with appropriate content disposition
                    return File(fileContent, "application/pdf", "PolicyDetails.pdf");
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

