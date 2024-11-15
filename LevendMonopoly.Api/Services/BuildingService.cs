﻿using LevendMonopoly.Api.Data;
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

        public async Task<IEnumerable<Building>> GetBuildingsAsync(Guid teamId)
        {
            var result = await _context.Buildings.Where(building => building.OwnerId == teamId).ToListAsync();
            return await _context.Buildings.Where(building => building.OwnerId == teamId).ToListAsync();
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

        public async Task ResetBuildingsState(Guid teamId)
        {
            var buildings = _context.Buildings.Where(b => b.OwnerId == teamId);
            await buildings.ForEachAsync(b => b.ResetToUnsoldState());

            _context.UpdateRange(buildings);
            await _context.SaveChangesAsync();
        }
    }
}