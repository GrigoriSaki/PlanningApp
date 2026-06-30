using Microsoft.EntityFrameworkCore;
using PlanningApp.Models;

namespace PlanningApp.Data
{
    public class PlanningDbContext : DbContext
    {
        public PlanningDbContext(DbContextOptions<PlanningDbContext> options) 
            : base (options)
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }
        public DbSet<ScheduleAssignment> ScheduleAssignments { get; set; }
    }
}
