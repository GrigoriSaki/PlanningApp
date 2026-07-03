using Microsoft.EntityFrameworkCore;
using PlanningApp.Data;
using PlanningApp.Models;

namespace PlanningApp.Services
{
    public class EmployeeService
    {
        private readonly IDbContextFactory <PlanningDbContext> _contextFactory;

        public EmployeeService(IDbContextFactory<PlanningDbContext> context) 
        {
            _contextFactory = context;
        }

        public async Task<List<Employee>> GetAllAsync() 
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.Employees.OrderBy(e => e.LastName).ToListAsync();
        }

        public async Task AddAsync(Employee employee) 
        {
            await using var context = _contextFactory.CreateDbContext();
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee) 
        {
            await using var context = _contextFactory.CreateDbContext();
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(Employee employee)
        {
            await using var context = _contextFactory.CreateDbContext();
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
        }
    }
}
