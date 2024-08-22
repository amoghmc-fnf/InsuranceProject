namespace InsuranceApi.DTOs
{
    public class PolicyHolderDto
    {
        public int PolicyHolderId { get; set; }
        public string Name { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Status { get; set; }
    }
}
