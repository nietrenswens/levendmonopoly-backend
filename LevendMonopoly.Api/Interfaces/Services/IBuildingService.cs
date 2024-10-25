using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetBuildings(Guid userId);
        Task<IEnumerable<Building>> GetBuildings();
        Task<Building?> GetBuilding(Guid buildingId);
        Task CreateBuilding(Building building);
        Task DeleteBuilding(Guid id);
        Task UpdateBuilding(Building building);
    }
}
