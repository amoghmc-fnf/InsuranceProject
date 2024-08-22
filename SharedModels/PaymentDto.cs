
namespace InsuranceApi.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int InsuredPolicyId { get; set; }
        public int PolicyHolderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
