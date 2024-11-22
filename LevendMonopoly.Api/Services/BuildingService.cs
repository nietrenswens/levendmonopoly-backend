using CsvHelper;
using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

namespace LevendMonopoly.Api.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly DataContext _context;

        public BuildingService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync(Guid teamId)
        {
            return await _context.Buildings.Where(building => building.OwnerId == teamId).OrderBy(b => b.Created).ToListAsync();
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            return await _context.Buildings.OrderBy(b => b.Created).ToListAsync();
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

        public async Task ResetBuildingsState(Guid teamId)
        {
            var buildings = _context.Buildings.Where(b => b.OwnerId == teamId);
            await buildings.ForEachAsync(b => b.ResetToUnsoldState());

            _context.UpdateRange(buildings);
            await _context.SaveChangesAsync();
        }

        public async Task ResetAllBuildings()
        {
            var buildings = await _context.Buildings.ToListAsync();
            buildings.ForEach(building =>
            {
                building.ResetToUnsoldState();
            });
            _context.UpdateRange(buildings);
            await _context.SaveChangesAsync();
        }

        public async Task<string> ExportAsJSON()
        {
            var buildings = await _context.Buildings.ToListAsync();
            var json = JsonSerializer.Serialize(buildings);
            return json;
        }

        public async Task<Result> ImportFromJSON(string data)
        {
            try
            {
                var buildings = JsonSerializer.Deserialize<Building[]>(data);
                if (buildings == null)
                    return Result.Failure("Het opgegeven bestand kan niet geladen worden");

                var result = _context.Buildings.ToList();

                foreach (var building in buildings)
                {
                    if (result.Any(b => b.Id == building.Id))
                    {
                        _context.Update(building);
                    }
                    else
                    {
                        _context.Add(building);
                    }

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
            return Result.Success();
        }
    }
}
