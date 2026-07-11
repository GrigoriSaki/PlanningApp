namespace PlanningApp.Planning
{
    public class PlanningCell
    {
        public DateTime WorkDate { get; set; }

        public int Shift { get; set; }

        public int ProductionLineId { get; set; }

        public string Position { get; set; } = "";
    }
}
