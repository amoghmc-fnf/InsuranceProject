namespace InsuranceApi.DTOs
{
    public class InsuranceTypeDto
    {
        public int InsuranceId { get; set; }
        public string InsuranceType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal BaseRate { get; set; }
        public int CoverageSize { get; set; }
    }
}
