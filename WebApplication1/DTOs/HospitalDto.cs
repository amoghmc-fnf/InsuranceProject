namespace InsuranceApi.DTOs
{
	public class HospitalDto
	{
		public int HospitalId { get; set; }
		public string Name { get; set; } = null!;
		public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
