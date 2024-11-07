using LevendMonopoly.Api.Models;

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
    }
}
