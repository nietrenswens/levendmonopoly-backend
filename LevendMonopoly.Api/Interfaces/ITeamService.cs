using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces
{
    public interface ITeamService
    {
        Task<bool> CreateTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(Team team, Guid teamId);
        Task<bool> DeleteTeamAsync(Guid teamId);
        Task<Team?> GetTeamAsync(Guid teamId);
        Task<Team?> GetTeamByNameAsync(string name);
        Task<List<Team>> GetAllTeamsAsync();
    }
}
