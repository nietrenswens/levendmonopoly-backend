using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly DataContext _context;

        public BuildingService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync(Guid userId)
        {
            var result = await _context.Buildings.Where(building => building.OwnerId == userId).ToListAsync();
            return await _context.Buildings.Where(building => building.OwnerId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            return await _context.Buildings.ToListAsync();
        }

        public async Task CreateBuildingAsync(Building building)
        {
            await _context.AddAsync(building);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBuildingAsync(Guid id)
        {
            var building = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id);
            if (building == null) return;
            _context.Remove(building);
            await _context.SaveChangesAsync();
        }

        public async Task<Building?> GetBuildingAsync(Guid buildingId)
        {
            return await _context.Buildings.FirstOrDefaultAsync(b => b.Id == buildingId);
        }

        public async Task UpdateBuildingAsync(Building building)
        {
            _context.Update(building);
            await _context.SaveChangesAsync();
        }
    }
}
