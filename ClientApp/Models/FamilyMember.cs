namespace MyClientApp.Models
{
    public class FamilyMember
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public int Age => CalculateAge(DOB);
        public List<string> HealtFactor {  get; set; }
        private int CalculateAge(DateTime birtDate)
        {
            int age=DateTime.Today.Year-birtDate.Year;
            if (birtDate.Date > DateTime.Today.AddYears(-age));
            return age;
        }
    }
}
