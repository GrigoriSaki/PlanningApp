using Microsoft.EntityFrameworkCore;
using PlanningApp.Data;
using PlanningApp.Models;

namespace PlanningApp.Services
{
    public class ProductionLineService
    {
        private readonly IDbContextFactory<PlanningDbContext> _contextFactory;

        public ProductionLineService(IDbContextFactory<PlanningDbContext> context)
        {
            _contextFactory = context;
        }

        public async Task<List<ProductionLine>> GetAllAsync()
        {
            await using var context = _contextFactory.CreateDbContext();
            return await context.ProductionLines.OrderBy(e => e.Name).ToListAsync();
        }
        public async Task AddAsync(ProductionLine productionLine)
        {
            await using var context = _contextFactory.CreateDbContext();
            context.ProductionLines.Add(productionLine);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(ProductionLine productionLine)
        {
            await using var context = _contextFactory.CreateDbContext();
            context.ProductionLines.Remove(productionLine);
            await context.SaveChangesAsync();
        }
        public async Task EditAsync(ProductionLine productionLine)
        {
            await using var context = _contextFactory.CreateDbContext();
            context.ProductionLines.Update(productionLine);
            await context.SaveChangesAsync();
        }
    }
}
