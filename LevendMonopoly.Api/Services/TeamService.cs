using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Services
{
    public class TeamService : ITeamService
    {
        private readonly DataContext _context;

        public TeamService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTeamAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTeamAsync(Guid teamId)
        {
            var team = await GetTeamAsync(teamId);
            if (team == null)
            {
                return false;
            }
            _context.Teams.Remove(team);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team?> GetTeamAsync(Guid teamId)
        {
            return await _context.Teams.FirstOrDefaultAsync(team => team.Id == teamId);
        }

        public async Task<Team?> GetTeamByNameAsync(string name)
        {
            return await _context.Teams.FirstOrDefaultAsync(team => team.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> UpdateTeamAsync(Team team, Guid teamId)
        {
            var teamToEdit = await GetTeamAsync(teamId);
            if (teamToEdit == null) 
            {
                return false;
            }
            teamToEdit.Name = team.Name;
            teamToEdit.PasswordHash = team.PasswordHash;
            teamToEdit.PasswordSalt = team.PasswordSalt;
            teamToEdit.Balance = team.Balance;
            teamToEdit.Buildings = team.Buildings;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
