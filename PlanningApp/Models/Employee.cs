namespace PlanningApp.Models
{
    public class Employee
    {
       public int Id { get; set; }
        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public string DefaultPosition { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
