using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface ITeamService
    {
        Task<bool> CreateTeamAsync(Team team);
        Task UpdateTeamAsync(Team team);
        Task<bool> DeleteTeamAsync(Guid teamId);
        Task<Team?> GetTeamAsync(Guid teamId);
        Task<Team?> GetTeamByNameAsync(string name);
        Task<List<Team>> GetAllTeamsAsync(bool includeBuildings = false);
    }
}
