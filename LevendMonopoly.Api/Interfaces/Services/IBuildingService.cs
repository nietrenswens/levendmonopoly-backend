using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetBuildings(Guid userId);
        Task<IEnumerable<Building>> GetBuildings();
        Task CreateBuilding(Building building);
    }
}
