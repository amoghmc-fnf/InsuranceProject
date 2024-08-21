namespace InsuranceApi.DTOs
{
    public class BlacklistDto
    {
        public int BlacklistId { get; set; }
        public int PolicyHolderId { get; set; }
        public int AdminId { get; set; }
        public DateTime BlacklistDate { get; set; }
        public string Reason { get; set; } = null!;
    }
}
