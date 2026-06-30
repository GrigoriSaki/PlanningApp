using PlanningApp.Data;

namespace PlanningApp.Services
{
    public class EmployeeService
    {
        private readonly PlanningDbContext _context;

        public EmployeeService(PlanningDbContext context) 
        {
            _context = context;
        }

    }
}
