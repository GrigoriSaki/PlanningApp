namespace PlanningApp.Models
{
    public class ScheduleAssignment
    {
        public int Id { get; set; }
        public DateTime WorkDate { get; set; }
        public string PositionOnLine { get; set; } = string.Empty;
        public int Shift { get; set; }
        public int ProductionLineId { get; set; }
        public ProductionLine? ProductionLine { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }


    }
}
