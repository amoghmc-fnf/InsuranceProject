namespace InsuranceApi.DTOs
{
	public class InsuredDto
	{
		public int InsuredId { get; set; }
		public int PolicyHolderId { get; set; }
		public string Name { get; set; } = null!;
		public DateTime Dob { get; set; }
		public string Gender { get; set; } = null!;
		public DateTime RegistrationDate { get; set; }
	}
}
