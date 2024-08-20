namespace InsuranceApi.DTOs
{
    public class AdminDto
    {
        public int AdminId { get; set; }

        public string Name { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
    }
}
