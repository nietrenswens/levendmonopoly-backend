﻿using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetBuildingsAsync(Guid teamId);
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<Building?> GetBuildingAsync(Guid buildingId);
        Task CreateBuildingAsync(Building building);
        Task DeleteBuildingAsync(Guid id);
        Task UpdateBuildingAsync(Building building);
        Task ResetBuildingsState(Guid teamId);
        Task ResetAllBuildings();
        Task<string> ExportAsJSON();
        Task<Result> ImportFromJSON(string data);
    }
}
