namespace InsuranceApi.DTOs
{
    public class PolicyDto
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = null!;
        public int InsuranceTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
    }
}
