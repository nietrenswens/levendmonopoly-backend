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

        public async Task<IEnumerable<Building>> GetBuildings(Guid userId)
        {
            var result = await _context.Buildings.Where(building => building.OwnerId == userId).ToListAsync();
            return await _context.Buildings.Where(building => building.OwnerId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Building>> GetBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }

        public async Task CreateBuilding(Building building)
        {
            await _context.AddAsync(building);
            await _context.SaveChangesAsync();
        }
    }
}
