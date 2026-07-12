using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PlanningApp.Data;
using PlanningApp.Models;

namespace PlanningApp.Services
{
    public class PlanningService
    {
        private readonly IDbContextFactory<PlanningDbContext> _contextFactory;
        public PlanningService(IDbContextFactory<PlanningDbContext> context)
        {
            _contextFactory = context;
        }
        public async Task<List<Models.ScheduleAssignment>> GetWeekPlanningAsync(DateTime anyDayInWeek) 
        {
            int diff = (7 + (anyDayInWeek.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = anyDayInWeek.AddDays(-diff).Date;
            DateTime endOfWeek = startOfWeek.AddDays(7).Date;

            await using var context = _contextFactory.CreateDbContext();
            return await context.ScheduleAssignments
                .Include(x => x.Employee)
                .Include(x => x.ProductionLine)
                .Where(e => e.WorkDate >= startOfWeek && e.WorkDate < endOfWeek)
                .OrderBy(x => x.WorkDate)
                .ThenBy(x => x.Shift)
                .ThenBy(x => x.ProductionLineId)
                .ThenBy(x => x.PositionOnLine)
                .ToListAsync ();
        }

        //Przypisanie pracownika z imienia i nazwiska do konkretnej komórki w planingu
        public async Task AssignEmployeeAsync(DateTime workDate, int shift, int productionLineId, string position, int? employeeId) 
        {
            var start = workDate.Date;  

            if (employeeId.HasValue) 
            {
                bool canAssign = await CanAssignEmployeeAsync(employeeId.Value, start, shift);

                if (!canAssign) 
                {
                    throw new InvalidOperationException(
                    "Employee is already assigned to another shift on this day.");
                }
            }

            await using var context = _contextFactory.CreateDbContext();
            var end = start.AddDays(1);
            var assignment = await context.ScheduleAssignments
                .FirstOrDefaultAsync(x => x.WorkDate >= start && x.WorkDate < end &&
                x.Shift == shift &&
                x.ProductionLineId == productionLineId &&
                x.PositionOnLine == position);

            if (assignment == null) 
            {
                var newAssignment = new Models.ScheduleAssignment
                {
                    WorkDate = start,
                    Shift = shift,
                    ProductionLineId = productionLineId,
                    PositionOnLine = position,
                    EmployeeId = employeeId
                };

                context.ScheduleAssignments.Add(newAssignment);
            }
            else 
            { 
                assignment.EmployeeId = employeeId; 
            }
            await context.SaveChangesAsync();
        }

        //Walidacja przed przypisaniem pracownika
        public async Task<bool> CanAssignEmployeeAsync(int employeeId, DateTime workDate, int shift)
        {
            await using var context = _contextFactory.CreateDbContext();

            var start = workDate.Date;
            var end = start.AddDays(1);

            bool alreadyAssigned = await context.ScheduleAssignments.AnyAsync(x =>
            x.EmployeeId == employeeId &&
            x.WorkDate >= start &&
            x.WorkDate < end &&
            x.Shift != shift);

            return !alreadyAssigned;
        }

        public async Task UnassignEmployeeAsync(DateTime workDate, int shift, int productionLineId, string position) 
        {
            var start = workDate.Date;
            var end = start.AddDays(1);

            await using var context = _contextFactory.CreateDbContext();

            var assignment = await context.ScheduleAssignments
            .FirstOrDefaultAsync(x => x.WorkDate >= start && x.WorkDate < end &&
                x.Shift == shift &&
                x.ProductionLineId == productionLineId &&
                x.PositionOnLine == position);

            if (assignment != null) 
            {
                context.ScheduleAssignments.Remove(assignment);
                await context.SaveChangesAsync();
            }
        }
    }
}
