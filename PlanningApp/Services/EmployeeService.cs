using Microsoft.EntityFrameworkCore;
using PlanningApp.Data;
using PlanningApp.Models;

namespace PlanningApp.Services
{
    public class EmployeeService
    {
        private readonly PlanningDbContext _context;

        public EmployeeService(PlanningDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync() 
        {
            return await _context.Employees.OrderBy(e => e.LastName).ToListAsync();
        }

        public async Task AddAsync(Employee employee) 
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee) 
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
