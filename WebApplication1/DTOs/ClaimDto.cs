namespace InsuranceApi.DTOs
{
    public class ClaimDto
    {
        public int ClaimId { get; set; }
        public int InsuredPolicyId { get; set; }
        public int PolicyHolderId { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ClaimStatus { get; set; } = null!;
        public decimal? DispenseAmount { get; set; }
        public string DocumentType { get; set; } = null!;
        public string DocumentPath { get; set; } = null!;
        public int HospitalId { get; set; }
    }
}
