namespace InsuranceApi.DTOs
{
    public class InsuredPolicyDto
    {
        public int InsuredPolicyId { get; set; }
        public int InsuredId { get; set; }
        public int PolicyId { get; set; }
        public string ApprovalStatus { get; set; } = null!;
        public string RenewalStatus { get; set; } = null!;
        public int AdminId { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
